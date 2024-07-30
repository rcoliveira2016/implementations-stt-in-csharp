using Microsoft.Extensions.Configuration;
using Vosk;

namespace VostSTT;

public class TranscreverAudio(IConfiguration configuration)
{
    public void Transcrever(){
        var pathModel = ObterPathModel();
        var model = new Model(pathModel);
        VoskRecognizer rec = new VoskRecognizer(model, 16000.0f);

        rec.SetMaxAlternatives(0);
        rec.SetWords(true);
        using (Stream source = File.OpenRead(ObterPathAudio()))
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                rec.AcceptWaveform(buffer, bytesRead);
            }
        }
        Console.WriteLine(rec.FinalResult());
    }

    public string ObterPathModel() {
        var pathSmall = configuration.GetValue<string>("Vosk:ModelPath:Small");
        if(pathSmall is null) return string.Empty;
        return pathSmall;
    }

    public string ObterPathAudio()
    {
        var pathAudio = configuration.GetValue<string>("Files:AudioExemple");
        if (pathAudio is null) return string.Empty;
        return pathAudio;
    }

}
