namespace App.Core.DomainServices
{
    public interface IParkingSensorService : IService
    {
        bool Started { get; }

        void Start();

        void Stop();
    }
}
