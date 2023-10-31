namespace Decorator_Wrapper
{
    public class Program
    {
        public abstract class MailServiceComponent
        {
            public abstract bool SendMail(string message);
        }

        public class OnPremiseMailService : MailServiceComponent
        {
            public override bool SendMail(string message)
            {
                Console.WriteLine($"Send Email on {nameof(OnPremiseMailService)} with messages: {message}");
                return true;
            }
        }

        public class CloudMailService : MailServiceComponent
        {
            public override bool SendMail(string message)
            {
                Console.WriteLine($"Send Email on {nameof(CloudMailService)} with message {message}");
                return true;
            }
        }

        public abstract class MailServiceDecoratorBase : MailServiceComponent
        {
            protected MailServiceComponent _iMailServiceComponent;
            public MailServiceDecoratorBase(MailServiceComponent iMailServiceComponent)
            {
                _iMailServiceComponent = iMailServiceComponent;
            }
            public override bool SendMail(string message)
            {
                _iMailServiceComponent.SendMail(message);
                return true;
            }
        }

        public class StatisticDecorator : MailServiceDecoratorBase
        {
            public StatisticDecorator(MailServiceComponent iMailServiceComponent) : base(iMailServiceComponent)
            {
            }

            public override bool SendMail(string message)
            {
                Console.WriteLine("Calculating to get statistic");
                base.SendMail(message);
                return true;
            }
        }

        public class MessageDatabaseDecorator : MailServiceDecoratorBase
        {
            public List<string> Messages { get; private set; } = new();
            public MessageDatabaseDecorator(MailServiceComponent iMailServiceComponent) : base(iMailServiceComponent)
            {
            }

            public override bool SendMail(string message)
            {
                if (base.SendMail(message))
                {
                    Messages.Add(message);  
                    return true;
                }
                return false;
                
            }
        }

        static void Main(string[] args)
        {
            OnPremiseMailService premiseMailService = new OnPremiseMailService();
            premiseMailService.SendMail("Hi there");

            CloudMailService cloudMailService = new CloudMailService();
            cloudMailService.SendMail("Hi there");

            MessageDatabaseDecorator decorator2 = new MessageDatabaseDecorator(premiseMailService);
            decorator2.SendMail("Hi There 1");
            decorator2.SendMail("Hi There 2");
            decorator2.SendMail("Hi There 3");

            foreach (var item in decorator2.Messages)
            {
                Console.WriteLine($"Saved Message: {item}");
            }

        }
    }
}