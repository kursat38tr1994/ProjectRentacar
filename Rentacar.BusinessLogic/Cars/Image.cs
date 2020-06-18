using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Rentacar.BusinessLogic.Cars
{
    public class Image
    {
        private readonly IWebHostEnvironment _webHostEnvironment ;

        public Image( IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        public void AddImage()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContent.Request.From.Files;
            
            
            string uploads = Path.Combine(webRootPath, @"images\cars");
            var extension = Path.GetExtension()
                string sdfsdf= HttpContent.Request
        }
    }
}
