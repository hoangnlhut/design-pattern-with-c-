using System.Transactions;

namespace Bridge
{
    public class Program
    {
        #region Bài toàn 1: thiết kế về thiết bị interface  và điều khiển để sử dụng các thiết bị đó 
        //public interface IDevice
        //{
        //    void GetInfomationOfDevice();
        //    bool IsEnable();
        //    void Enable();
        //    void Disable();
        //    int GetVolume();
        //    void SetVolume(int volume);
        //    int GetChannel();
        //    void SetChannel(int channel);
        //}

        //public class Tv : IDevice
        //{
        //    public bool Status { get ; set; }
        //    public int Channel { get; set; }
        //    public int Volume { get; set; }
        //    public Tv()
        //    {
        //        Status = false;
        //        Channel = 1;
        //        Volume = 20;
        //    }

        //    public void GetInfomationOfDevice()
        //    {
        //        Console.WriteLine($"Information now: Status: {Status}, Channel: {Channel}, Volume:{Volume} ");
        //    }

        //    public void Disable()
        //    {
        //        Status = false;
        //    }

        //    public void Enable()
        //    {
        //        Status= true;
        //    }

        //    public int GetChannel()
        //    {
        //        return Channel;
        //    }

        //    public int GetVolume()
        //    {
        //        return Volume;
        //    }

        //    public bool IsEnable()
        //    {
        //       return Status;
        //    }

        //    public void SetChannel(int channel)
        //    {
        //        Channel = channel;
        //    }

        //    public void SetVolume(int volume)
        //    {
        //       Volume = volume;
        //    }
        //}

        //public class Remote
        //{
        //    protected IDevice _device;
        //    public Remote(IDevice device)
        //    {
        //        _device = device;
        //    }

        //    public void ToglePower()
        //    {
        //        if (_device.IsEnable())
        //        {
        //            _device.Disable();
        //            Console.WriteLine("Device is disable");
        //        }
        //        else
        //        {
        //            _device.Enable();
        //            Console.WriteLine("Device is enable");

        //        }
        //    }

        //    public void GetInfoOfDevice()
        //    {
        //        _device.GetInfomationOfDevice();
        //    }

        //    public void VolumeDown()
        //    {
        //        _device.SetVolume(_device.GetVolume() - 10);
        //    }

        //    public void VolumeUp()
        //    {
        //        _device.SetVolume(_device.GetVolume() + 10);
        //    }

        //    public void ChannelDown()
        //    {
        //        _device.SetChannel(_device.GetChannel() - 1);
        //    }

        //    public void ChannelUp()
        //    {
        //        _device.SetChannel(_device.GetChannel() + 1);
        //    }

        //    public int GetChannel() => _device.GetChannel();
        //    public int GetVolume() => _device.GetVolume();
        //}

        //public class AdvancedRemote : Remote
        //{
        //    public AdvancedRemote(IDevice device) : base(device)
        //    {
        //    }

        //    public void Mute()
        //    {
        //        _device.SetVolume(0);
        //        Console.WriteLine($"Volume is mute {_device.GetVolume()}");
        //    }
        //}

        //static void Main(string[] args)
        //{

        //    Console.WriteLine("Using ordinary remote to operate Tv");
        //    Remote ordinary = new Remote(new Tv());
        //    ordinary.GetInfoOfDevice();
        //    ordinary.ToglePower();
        //    Console.WriteLine($"Tv's channel now is {ordinary.GetChannel()}");
        //    ordinary.ChannelUp();
        //    Console.WriteLine($"Tv's channel after up is {ordinary.GetChannel()}");
        //    Console.WriteLine("End of using ordinary remote to operate Tv");
        //    ordinary.GetInfoOfDevice();
        //    Console.WriteLine("--------------------------------------------------");

        //    Console.WriteLine("Using advanded remote to operate Tv");
        //    AdvancedRemote advanded = new AdvancedRemote(new Tv());
        //    advanded.GetInfoOfDevice();
        //    advanded.ToglePower();
        //    Console.WriteLine($"Tv's channel now is {advanded.GetChannel()}");
        //    advanded.ChannelUp();
        //    Console.WriteLine($"Tv's channel after up is {advanded.GetChannel()}");

        //    Console.WriteLine($"Tv's Volume now is {advanded.GetVolume()}");
        //    advanded.VolumeUp();
        //    Console.WriteLine($"Tv's Volume after up is {advanded.GetVolume()}");
        //    advanded.Mute();
        //    advanded.GetInfoOfDevice();
        //    Console.WriteLine("End of Using advanded remote to operate Tv");

        //}
        #endregion

        #region Bài toàn 2: trên youtube có thể thanh toán qua momo và thẻ credicard : ta khai báo lớp interface implementation có thể thành toán qua momo  và credit card , đồng thời ta khai báo thêm lớp abstraction class để đăng ký thành viên, khi đăng ký thành viên thì sẽ thực hiện thanh toán qua momo / credit card sử dụng phương thức của interface implementation

        public interface IPaymentProcessor
        {
            void Payment(double amount);    
        }

        public class CreditPaymentProcess : IPaymentProcessor
        {
            private string CardNumber;
            private DateTime ExprireDate;
            private string Cvv;
            public CreditPaymentProcess(string cardNumber, DateTime exprireDate, string cvv)
            {
                CardNumber = cardNumber;
                ExprireDate = exprireDate;
                Cvv = cvv;
            }

            public void Payment(double amount)
            {
                Console.WriteLine($"Action somelogic of payment of Credit Card in card number : {CardNumber} with amount: {amount}");
            }
        }
       
        public class MomoPaymentProcess : IPaymentProcessor
        {
            private string PhoneNumber;
            public MomoPaymentProcess(string phoneNumber)
            {
                PhoneNumber = phoneNumber;
            }

            public void Payment(double amount)
            {
                Console.WriteLine($"Action somelogic of payment of momo in phone number : {PhoneNumber} with amount: {amount}");
            }
        }

        public class MemberRegistration
        {
            protected IPaymentProcessor _paymentProcessor;

            public MemberRegistration(IPaymentProcessor payment)
            {
                _paymentProcessor = payment;
            }

            public void Registration(double amount) 
            {
                Console.WriteLine($"Starting of Registration with amount: {amount}");
                _paymentProcessor.Payment(amount);
                Console.WriteLine("Finish to registration to become a member");
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("A people want to pay in credit card to registration");
            MemberRegistration creditRegistration = new MemberRegistration(new CreditPaymentProcess("123 432 323 3223", DateTime.Now, "356"));
            creditRegistration.Registration(100);

            Console.WriteLine("--------------------------");

            Console.WriteLine("A people want to pay in momo to registration");
            MemberRegistration momoRegistration = new MemberRegistration(new MomoPaymentProcess("0936264928"));
            momoRegistration.Registration(100);

        }
        #endregion
    }
}