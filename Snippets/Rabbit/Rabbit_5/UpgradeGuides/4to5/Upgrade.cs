﻿using System.Configuration;
using NServiceBus;

partial class Upgrade
{
    void CustomConnectionStringName(EndpointConfiguration endpointConfiguration)
    {
        #region 4to5CustomConnectionStringName

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        var connectionSettings = ConfigurationManager.ConnectionStrings["MyConnectionStringName"];
        transport.ConnectionString(connectionSettings.ConnectionString);

        #endregion
    }

    void UseDirectRoutingTopologyWithCustomConventions(EndpointConfiguration endpointConfiguration)
    {
        #region 4to5usedirectroutingtopology

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.UseDirectRoutingTopology(
            routingKeyConvention: type => "myroutingkey",
            exchangeNameConvention: () => "MyTopic");

        #endregion
    }
}