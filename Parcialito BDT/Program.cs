// Parcialito de prueba

string numeroDeSocio;
int[] planes = new int[6];
string[] nombrePlanes = new string[6] { "Basico Individual", "Basico Familiar", "Gold Individual", "Gold Familiar", "Platinum Individual", "Platinum Familiar" };
string[] codigoPlanes = new string[6] { "BI", "BF", "GI", "GF", "PI", "PF" };
int totalEstudios;
int totalEstudiosNoRealizados;
int copagos;
var totalEstudiosxMes = 0;
var totalEstudiosxMesNoRealizados = 0;

//Inicio General

IniciarPlanes(planes);

for (int i = 0; i < 3; i++)
{
    totalEstudios = 0;
    totalEstudiosNoRealizados = 0;
    copagos = 0;
    numeroDeSocio = IngresarSocio();
    while (numeroDeSocio != "-1")
    {
        totalEstudios++;
        var indicePlan = IngresarPlan(codigoPlanes);
        var estudio = IngresarEstudio();
        var fecha = IngresarFecha();
        var pago = Autorizar(codigoPlanes[indicePlan], estudio, fecha);
        if (pago > 0)
        {
            SumarAPlan(indicePlan, pago);
            copagos += pago;
        }
        else if (pago == -1)
        {
            totalEstudiosNoRealizados++;
        }
        numeroDeSocio = IngresarSocio();
    };
    CalcularPorcentajeNoRealizados(totalEstudios, totalEstudiosNoRealizados);
    Console.WriteLine($"El total recaudado por copagos fue de {copagos}");
    totalEstudiosxMes += totalEstudios;
    totalEstudiosxMesNoRealizados += totalEstudiosNoRealizados;
}
Console.WriteLine("--------------------");
Console.WriteLine("Totales:");
CalcularPorcentajeNoRealizados(totalEstudiosxMes, totalEstudiosxMesNoRealizados);
CalcularMayorRecaudacion();


//--------- Funciones ----------


void CalcularPorcentajeNoRealizados(int total, int parcial)
{
    int result = 0;
    if (total != 0)
    {
       result = (parcial * 100) / total;
    }

    Console.WriteLine($"El total porcentaje de estudios no realizados es de {result}%");
}

void CalcularMayorRecaudacion()
{
    int resul = 0;
    int index = 0;
    for (int i = 0; i < planes.Length; i++)
    {
        if (planes[i] > resul)
        {
            resul = planes[i];
            index = i;
        }
    }

    Console.WriteLine($"El plan con mayor recaudacion fue {nombrePlanes[index]} con ${resul}");
}

DateTime IngresarFecha()
{
    Console.WriteLine("Ingrese una fecha con el sig formato dd/mm/yyyy");
    var resp = Console.ReadLine();
    //var fecha = DateTime.Parse(resp);
    var fecha = DateTime.Now;

    return fecha;
}

string IngresarEstudio()
{
    Console.WriteLine("Ingrese el estudio a realizar");
    var estudio = Console.ReadLine();
    while (estudio.Length < 5)
    {
        Console.WriteLine("Error, vuelva a ingresar el estudio");
        estudio = Console.ReadLine();
    };

    return estudio;
}

int Autorizar(string plan, string estudio, DateTime fecha)
{
    var rnd = new Random();
    var result = rnd.Next(-2, 2);
    if (result > 0)
    {
        result = rnd.Next(100000, 999999);
    }

    return result;
}

int IngresarPlan(string[] arr)
{
    int indice;
    do
    {
        Console.WriteLine("Ingrese su plan");
        var plan = Console.ReadLine().ToUpper();
        indice = BuscarPlan(plan, arr);
    } while (indice == -1);

    return indice;
}

int BuscarPlan(string nombre, string[] arr)
{
    int res=-1;
    for (int i = 0; i<arr.Length; i++)
    {
        if (arr[i] == nombre)
        {
            res = i;
        }
    }

    return res;
};

void IniciarPlanes(int[] arr)
{
    for (int i = 0; arr.Length > i; i++)
    {
        arr[i] = 0;
    }
}

void SumarAPlan(int plan, int pesos)
{
   planes[plan] += pesos;
}

string IngresarSocio()
{
    Console.WriteLine("Ingrese el numero de Socio");
    var numero = Console.ReadLine();
    int result;
    var esNumero = int.TryParse(numero, out result);
    while (!(esNumero && (result == -1 || Math.Abs(result) >= 100000)))
    {
        Console.WriteLine("Error, vuelva a ingresar el numero de socio");
        numero = Console.ReadLine();
        esNumero = int.TryParse(numero, out result);
    }
    return numero;
}
