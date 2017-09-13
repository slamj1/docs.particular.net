﻿using NServiceBus;
using NServiceBus.Logging;

public class EndpointConfig :
#pragma warning disable 618
    IConfigureThisEndpoint
#pragma warning restore 618
{
    static EndpointConfig()
    {
        var defaultFactory = LogManager.Use<DefaultFactory>();
        defaultFactory.Level(LogLevel.Debug);
    }

    public void Customize(EndpointConfiguration endpointConfiguration)
    {
        var persistence = endpointConfiguration.UsePersistence<AzureStoragePersistence>();
        persistence.ConnectionString("UseDevelopmentStorage=true");

        #region AzureMultiHost_MessageMapping

        var transport = endpointConfiguration.UseTransport<AzureStorageQueueTransport>();
        var routing = transport.Routing();
        routing.RouteToEndpoint(
            messageType: typeof(Ping),
            destination: "Receiver");

        #endregion

        transport.ConnectionString("UseDevelopmentStorage=true");
        transport.SerializeMessageWrapperWith<NewtonsoftSerializer>();
        transport.DelayedDelivery().DisableTimeoutManager();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.DisableNotUsedFeatures();
    }
}