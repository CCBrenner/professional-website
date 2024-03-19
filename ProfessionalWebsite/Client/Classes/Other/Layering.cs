namespace ProfessionalWebsite.Client.Classes;

public class Layering
{
    public Layering(int numberOfLayers) 
    {
        List<Layer> layers = new List<Layer>();
        for (int i = 0; i < numberOfLayers; i++)
            layers.Add(new Layer());
        Layers = layers;
    }
    public List<Layer> Layers;
}
public class Layer
{
    public Layer() { }
    public int ZIndexValue;

}
