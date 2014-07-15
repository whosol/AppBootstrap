namespace Actemium.Stratus.MailboxPlugin.Enums
{
    public enum ResponseCode : byte
    {
        AllOk = 0x00,
        RejectRequest = 0xFF,
        InvalidChecksum = 0xFE,
        ResultResponsePending = 0x01,
        UnknownXmlFormat = 0x02,
        DuplicateResultsReceived = 0x03,
        DataNotXml = 0x03,
    }
}
