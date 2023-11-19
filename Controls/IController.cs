namespace IDSP_Boiler_Plate;

public interface IController<T, C>
{   
    T Process(C Input);
}
