﻿//
// Copyright 2015 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Collections.Generic;

namespace Steeltoe.CloudFoundry.Connector.Services
{
    public class RabbitServiceInfoFactory : ServiceInfoFactory
    {
        private static string[] _scheme = new string[] { RabbitServiceInfo.AMQP_SCHEME, RabbitServiceInfo.AMQPS_SCHEME };
        public RabbitServiceInfoFactory() :
            base(new Tags("rabbit"), _scheme)
        {

        }
        public override IServiceInfo Create(Service binding)
        {
            string uri = GetUriFromCredentials(binding.Credentials);
            string managementUri = GetStringFromCredentials(binding.Credentials, "http_api_uri");

            if (binding.Credentials.ContainsKey("uris"))
            {
                List<string> uris = GetListFromCredentials(binding.Credentials, "uris");
                List<string> managementUris = GetListFromCredentials(binding.Credentials, "http_api_uris");
                return new RabbitServiceInfo(binding.Name, uri, managementUri, uris, managementUris);
            }

            return new RabbitServiceInfo(binding.Name, uri, managementUri);
        }
    }
}
