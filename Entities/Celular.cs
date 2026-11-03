namespace Parcial.Entities;

public class Celular
{
    private int id;
    private string marca;
    private string modelo;
    private int precio;

    public int Id { get => id; set => id = value; }
    public string Marca { get => marca; set => marca = value; }
    public string Modelo { get => modelo; set => modelo = value; }
    public int Precio { get => precio; set => precio = value; }
}
