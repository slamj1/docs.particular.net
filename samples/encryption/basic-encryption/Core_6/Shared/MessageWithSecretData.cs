﻿using System;
using System.Collections.Generic;
using NServiceBus;

#region Message
#pragma warning disable 618

public class MessageWithSecretData :
    IMessage
{
    public WireEncryptedString Secret { get; set; }
    public MySecretSubProperty SubProperty { get; set; }
    public List<CreditCardDetails> CreditCards { get; set; }
}

public class MySecretSubProperty
{
    public WireEncryptedString Secret { get; set; }
}

public class CreditCardDetails
{
    public DateTime ValidTo { get; set; }
    public WireEncryptedString Number { get; set; }
}
#endregion