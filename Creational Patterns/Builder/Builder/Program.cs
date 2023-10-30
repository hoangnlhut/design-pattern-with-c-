namespace Builder
{
    public class Program
    {
        public class Car
        {
            public string name { get; set; }
            public string engine { get; set; }
            public int sit { get; set; }
            public bool IsSetGPS { get; set; }

            public override string ToString()
            {
                
                return name + "car with engine: " + engine + " number of sit: " + sit + " and Set GPS:" + (IsSetGPS ? "yes" : "no");
            }
        }

        public interface ICarBuilder
        {
            public void Reset();
            public void SetName(string name);
            public void SetEngine(string engine);
            public void SetSit(int sit);
            public void SetGps(bool Gps);
        }

        public class CarBuilder : ICarBuilder
        {
            private Car car = new Car();

            public CarBuilder()
            {
                Reset();
            }

            public void Reset()
            {
                car = new Car();
            }

            public void SetEngine(string engine)
            {
                car.engine = engine;
            }

            public void SetGps(bool Gps)
            {
                car.IsSetGPS = Gps;
            }

            public void SetName(string name)
            {
                car.name = name;
            }

            public void SetSit(int sit)
            {
                car.sit = sit;
            }

            public Car GetResult()
            {
                return car;
            }
        }

        public class CarDirector
        {
            public void Car2Sit(ICarBuilder builder)
            {
                builder.Reset();
                builder.SetName("BWM");
                builder.SetSit(2);
                builder.SetEngine("V8 BMW");
                builder.SetGps(true);
            }

            public void Car4Sit(ICarBuilder builder)
            {
                builder.Reset();
                builder.SetName("Range Rover");
                builder.SetSit(4);
                builder.SetEngine("V12 Range Rover");
                builder.SetGps(true);
            }
        }
        

        public static void Main(string[] args)
        {
            CarDirector director = new CarDirector();
            var carbuilder = new CarBuilder();

            Console.WriteLine("Build car 2 sit BMW");
            director.Car2Sit(carbuilder);
            Console.WriteLine($"Result:{carbuilder.GetResult().ToString()}");

            Console.WriteLine("Build car 4 sit BMW");
            director.Car4Sit(carbuilder);
            Console.WriteLine($"Result:{carbuilder.GetResult().ToString()}");
        }
    }
} 