using AutoMapper;
using Metamask.Axioms;
using Metamask.Data;
using Metamask.Web.Configuration;
using Metamask.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Metamask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPageMaskRepository _repository;
        private readonly IOptions<AppSettings> _settings;

        public HomeController(
            IMapper mapper,
            IPageMaskRepository repository,
            IOptions<AppSettings> settings)
        {
            _mapper = mapper;
            _repository = repository;
            _settings = settings;
        }

        /// <summary>
        /// Home page of the application.
        /// </summary>
        /// <returns>A form allowing the user to create a page mask.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Postback for the homepage that creates a page mask for
        /// the user.
        /// </summary>
        /// <param name="input">Details about the page mask.</param>
        /// <returns>A page with errors on failure or a redirection
        /// to the success page for the user to get more information.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexInputModel input)
        {
            if(ModelState.IsValid)
            {
                var mask = _mapper.Map<PageMask>(input);
                await _repository.InsertPageMaskAsync(mask);
                var shortId = GuidEncoder.Encode(mask.PageMaskId);
                return Redirect($"/success?id={shortId}");
            }
            
            var model = new IndexViewModel()
            {
                Input = input
            };
            return View(model);
        }

        /// <summary>
        /// Success page shown allowing the user to get their link to share.
        /// We do a success redirection to lessen the amount of accidential
        /// re-post reqeusts.
        /// </summary>
        /// <param name="id">The encoded PageMaskId to generate a link.</param>
        /// <returns>A page with instructions on how to use their new link.</returns>
        [HttpGet("/success")]
        public IActionResult Success(string id)
        {
            var model = new SuccessViewModel()
            {
                MaskedPageUrl = $"{_settings.Value.Hostname}{id}"
            };
            return View(model);
        }

        /// <summary>
        /// Page that to a robot looks like it is full of meta data. It then redirects
        /// the user to a final destination previously specified.
        /// </summary>
        /// <param name="id">Encoded guid of the pagemask previously created.</param>
        /// <returns>A page with </returns>
        [HttpGet("/{id}")]
        public async Task<IActionResult> MaskPage(string id)
        {
            Guid pageMaskId;
            try
            {
                pageMaskId = GuidEncoder.Decode(id);
            }
            catch(Exception e)
            {
                if(e is ArgumentException || e is FormatException)
                {
                    return NotFound();
                }
                throw;
            }

            var pageMask = await _repository.GetPageMaskByIdAsync(pageMaskId);
            if (pageMask == null)
                return NotFound();

            var model = new MaskPageViewModel()
            {
                PageMask = pageMask,
                Url = $"{_settings.Value.Hostname}{id}"
            };
            return View(model);
        }

        /// <summary>
        /// Default error page generated in .NET.
        /// </summary>
        /// <returns>Page with details about an error that is thrown.</returns>
        [ResponseCache(Duration = 0, 
            Location = ResponseCacheLocation.None, 
            NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(model);
        }

        /// <summary>
        /// Custom 404 error page created.
        /// </summary>
        /// <returns>Page letting the customer know there was an error.</returns>
        [Route("/error/404", Order = 999)]
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
