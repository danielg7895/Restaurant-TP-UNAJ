using Application.Services;
using System;

namespace Application
{
    public class Menu
    {
        public static void Show()
        {
            string response = "";

            while (response != "5")
            {
                Console.WriteLine("1. Registrar la mercadería (platos, bebida o postre).");
                Console.WriteLine("2. Registrar las comandas (el pedido del cliente)");
                Console.WriteLine("3. Enlistar las comandas con el detalle de los platos");
                Console.WriteLine("4. Enlistar la información de la mercadería");
                Console.WriteLine("5. Salir");

                response = ValidateInput(Console.ReadLine());

                Console.WriteLine(response);
            }
        }

        static string ValidateInput(string input)
        {
            try
            {
                return int.Parse(input) switch
                {
                    1 => MercaderiaService.AddMercaderia(),
                    2 => ComandaService.AddComanda(),
                    3 => ComandaService.GetComandasAsString(),
                    4 => MercaderiaService.GetMercaderiasAsString() ?? "No hay mercaderias registradas",
                    5 => "5",
                    _ => "Opcion Invalida, reintentar.",
                };

            }
            catch
            {
                return "Formato incorrecto!\n";
            }
        }

    }
}
