using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovarianceAndContravarianceDelegateExample.CovarianceAndContravariance
{
    public static class CovarianceAndContravariance
    {
        delegate Car CarFactoryDel(int id, string name);
        delegate void LogICECarDetails(ICECar car);
        delegate void LogEVCarDetails(EVCar car);
        public static void Example()
        {
            Console.WriteLine("********COVARIANCE EXAMPLE****************");

            CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;

            Car iceCar = carFactoryDel(1, "Audi R8");


            Console.WriteLine($"Object Type: {iceCar.GetType()}");
            Console.WriteLine($"Car Details: {iceCar.GetCarDetails()}");

            carFactoryDel = CarFactory.ReturnEV;

            Console.WriteLine();

            Car evCar = carFactoryDel(2, "Tesla Model-3");

            Console.WriteLine($"Object Type: {evCar.GetType()}");
            Console.WriteLine($"Car Details: {evCar.GetCarDetails()}");

            Console.WriteLine("********CONTRAVARIANCE EXAMPLE****************");

            LogICECarDetails logICECarDetailsDel = LogCarDetails;
            logICECarDetailsDel(iceCar as ICECar);

            LogEVCarDetails logEVCarDetailsDel = LogCarDetails;
            logEVCarDetailsDel(evCar as EVCar);

            Console.ReadKey();
        }

        static void LogCarDetails(Car car)
        {
            if (car is ICECar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICEDetails.txt"), true))
                {
                    sw.WriteLine($"Object Type: {car.GetType()}");
                    sw.WriteLine($"Car Details: {car.GetCarDetails()}");
                }
            }
            else if (car is EVCar)
            {
                Console.WriteLine($"Object Type: {car.GetType()}");
                Console.WriteLine($"Car Details: {car.GetCarDetails()}");
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static class CarFactory
        {
            public static ICECar ReturnICECar(int id, string name)
            {
                return new ICECar { Id = id, Name = name };
            }

            public static EVCar ReturnEV(int id, string name)
            {
                return new EVCar { Id = id, Name = name };
            }
        }

        public abstract class Car
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public virtual string GetCarDetails()
            {
                return $"{Id} - {Name}";
            }
        }

        public class ICECar : Car
        {
            public override string GetCarDetails()
            {
                return $"{base.GetCarDetails()} - Internal Combustion Engine";
            }
        }

        public class EVCar : Car
        {
            public override string GetCarDetails()
            {
                return $"{base.GetCarDetails()} - eLECTRIC";
            }
        }
    }
}
