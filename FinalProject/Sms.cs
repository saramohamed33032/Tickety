using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace FinalProject
{
    public class Sms
    {

        string _PhoneNumber;
        public Sms()
        {
            const string accountSid = "AC8524755927cf441b881d4e376d93856a";
            const string authToken = "43a0465b534704f8d2537657da435f89";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
            TwilioClient.Init(accountSid, authToken);

        }

        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }

            set
            {
                _PhoneNumber = value;
            }
        }
        public bool sendsms()
        {
            CreateVerificationOptions s = new CreateVerificationOptions("VA9d1daf8b5346fb512d19ce6da276ca23", _PhoneNumber, "sms");
            s.Locale = "en";
            var x = VerificationResource.Create(s);
            if (x.Status == "pending" || x.Status == "approved")
                return true;
            else
                return false;

        }
        public bool? verify(string codeverifi)
        {
         
          
            try
            {
                var verificationCheck = VerificationCheckResource.Create(
            //  to: "+2001094869002",
            to: _PhoneNumber,
           code: codeverifi,
           pathServiceSid: "VA9d1daf8b5346fb512d19ce6da276ca23"
       );

               
                return verificationCheck.Valid;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}