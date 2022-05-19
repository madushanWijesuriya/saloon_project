using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        [HttpGet, Authorize]
        public IActionResult GenerateQR()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded." + userId, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20, darkColor: Color.Black, lightColor: Color.White, true, imgType: Base64QRCode.ImageType.Png);
            return Ok(qrCodeImageAsBase64);
        }
    }
}
