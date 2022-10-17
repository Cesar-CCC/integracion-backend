using MimeKit;
using MailKit.Net.Smtp;

namespace sgc_backend.Utils
{
    public class Correo
    {
        public string EnviarCorreo(ValoresCorreo valoresCorreo)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(valoresCorreo.FromName, valoresCorreo.FromAddress));
            message.To.Add(new MailboxAddress(valoresCorreo.ToName, valoresCorreo.ToAddress));
            message.Subject = valoresCorreo.Subject;

            message.Body = new TextPart("html")
            {
                //Text = $"<h4>{valoresCorreo.BodyTitle}</h4>" +
                //$"<p>{valoresCorreo.BodyMessage}</p>" +
                //$"<p><em><span style='color: #ff0000;'><strong>hola</strong></span></em></p>" +
                //$"<a sytle='color: red' href='{valoresCorreo.BodyLink}'>Link: {valoresCorreo.BodyLinkName}</a>" +
                //$"---------------------------------------------" +
                //$"<table style='height: 379px; width: 100%; border-collapse: collapse; border-style: none; margin-left: auto; margin-right: auto;' border='0'><tbody><tr><td style='width: 100%; height: 59px; background-color: gray;'><img style='display: block; margin-left: auto; margin-right: auto;' src='https://upload.wikimedia.org/wikipedia/commons/c/cb/Logo_UNAP.png' alt=' width='60' height='65' /></td></tr><tr><td style='width: 100%; height: 320px; background-color: red;'>sdf</td></tr></tbody></table>"

                Text = $"<!DOCTYPE html><html><head>	<title>correo</title></head><body style='margin: 0px 30%;  padding: 0px'>	<div style='background: red; text-align: center;'>		<div style='background: rgb(10,20,38);background: linear-gradient(333deg, rgba(10,20,38,1) 5%, rgba(45,99,200,1) 46%, rgba(17,40,83,1) 78%);'>			<div style='position: relative; height: 120px;'>				<table style='height: 18px; width: 100%; border-collapse: collapse;' border='0'>					<tbody>						<tr style='height: 18px;'>						<td style='width: 30%; height: 18px;'><img style='display: inline-block;margin-top: 20px;float: right; margin-right: 20px;' src='https://upload.wikimedia.org/wikipedia/commons/c/cb/Logo_UNAP.png' alt='' width='69' height='75' /></td>						<td style='width: 50%; height: 18px; text-align: left;color: white;font-family: Arial;'><p style='font-size: 18px;display: inline-block;margin-top: 45px;'>Universidad Nacional Del Altiplano</p></td>						</tr>					</tbody>				</table>								</div>		</div>	</div>	<div style='margin: 10% 12%; font-size: 16px; font-family: Arial; border: solid rgb(10,20,38) 0px;'>		<div style='margin: 5%;color: black'>			<span style='font-style: italic; color: gray; font-size: 14px'>{valoresCorreo.BodyTitle}</span>			<p><strong>Bienvenido/a</strong></p>			<p>{valoresCorreo.BodyMessage}</p>			<div style='text-align: center;'>				<a href='{valoresCorreo.BodyLink}' style='text-decoration: none;display: inline-block;margin-top: 50px;font-family: Arial;background-color: rgb(45, 99, 200);color: rgb(255,255,255); font-size: 17px; border: 0px solid rgb(10,20,38); border-radius: 0px; padding: 15px 50px;cursor: pointer; width: 50%'>{valoresCorreo.BodyLinkName}</a>			</div>		</div>	</div></body></html>"
            };
            //******** diseñar bien  

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(valoresCorreo.FromAddress, "hoewspwizvbhmddu");

                client.Send(message);
                client.Disconnect(true);
            }
            return "fds";
        }
    }
    public struct ValoresCorreo
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string ToName { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string BodyTitle { get; set; }
        public string BodyMessage { get; set; }
        public string BodyLink { get; set; }
        public string BodyLinkName { get; set; }
    }
}
