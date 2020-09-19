using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.Domain.Entities;
using OnLineShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Concrete
{
   public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessorOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using(var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.PassWord);

                StringBuilder builder = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items");
                foreach(var line in cart.Shopping)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    builder.AppendFormat("{0}x{1}(subtotal: {2:c}\n",
                                        line.Quantity,
                                        line.Product.Name,
                                        subtotal);
                }
                builder.AppendFormat("Total Order value : {0:c}",
                    cart.ComputeTotalPrice())
                    .AppendLine("---")
                    .AppendLine("Ship to")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "yes" : "No");
                MailMessage message = new MailMessage(emailSettings.MailFromAddress,
                                                      emailSettings.MailToAddress,
                                                      "New Order Submitted",
                                                      builder.ToString());
                smtpClient.Send(message);
            }
        }
    }
}
