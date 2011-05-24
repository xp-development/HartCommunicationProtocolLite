namespace Finaltec.Communication.HartLite
{
    public interface IAddress
    {
        byte[] ToByteArray();
        void SetNextByte(byte nextByte);

        byte this[int index] { get; }
    }
}