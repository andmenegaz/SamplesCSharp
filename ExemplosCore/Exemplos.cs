using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ExemplosCore
{
    //public class Exemplos
    //{
    //    private static Surface.Logger.Logger _surfaceLogger;
    //    private static Surface.Mqtt.Mqtt _mqtt;
    //    private static readonly IConfiguration Configuration;

    //    public static void CompararArquivos()
    //    {
    //        string dir_atual = @"C:\Users\andme\Downloads\laudit_atual\org";
    //        string dir_producao = @"C:\Users\andme\Downloads\laudit_produ\org";
    //        List<string> laudit_atual = new List<string>();
    //        List<string> laudit_producao = new List<string>();
    //        laudit_atual.AddRange(Directory.GetFiles(dir_atual, "*.*", SearchOption.AllDirectories));
    //        laudit_producao.AddRange(Directory.GetFiles(dir_producao, "*.*", SearchOption.AllDirectories));

    //        List<FileInfo> files_atual = new List<FileInfo>();
    //        List<FileInfo> files_producao = new List<FileInfo>();

    //        for (int i = 0; i < laudit_atual.Count; i++)
    //        {
    //            files_atual.Add(new FileInfo(laudit_atual[i]));
    //            laudit_atual[i] = laudit_atual[i].Substring(dir_atual.Length);
    //        }

    //        for (int i = 0; i < laudit_producao.Count; i++)
    //        {
    //            files_producao.Add(new FileInfo(laudit_producao[i]));

    //            laudit_producao[i] = laudit_producao[i].Substring(dir_producao.Length);
    //        }

    //        Console.WriteLine("ARQUIVOS PRODUCAO");
    //        laudit_producao.ForEach(f =>
    //        {
    //            string r = laudit_atual.Find(w => w.Equals(f));
    //            if (r == null)
    //            {
    //                Console.WriteLine(f);
    //            };
    //        });

    //        Console.WriteLine("ARQUIVOS ATUAL");
    //        laudit_atual.ForEach(f =>
    //        {
    //            string r = laudit_producao.Find(w => w.Equals(f));
    //            if (r == null)
    //            {
    //                Console.WriteLine(f);
    //            };
    //        });

    //        Console.WriteLine("COMPARE");
    //        laudit_atual.ForEach(f =>
    //        {
    //            string hash_atual = CalculateMD5(dir_atual + f);
    //            string hash_producao = CalculateMD5(dir_producao + f);

    //            if (hash_atual != hash_producao)
    //            {
    //                Console.WriteLine(f);
    //            }
    //        });
    //    }

    //    public static string CalculateMD5(string filename)
    //    {
    //        using (var md5 = MD5.Create())
    //        {
    //            using (var stream = File.OpenRead(filename))
    //            {
    //                var hash = md5.ComputeHash(stream);
    //                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    //            }
    //        }
    //    }

    //    public static string ReceberMQTT()
    //    {
    //        var services = new ServiceCollection();

    //        services.AddTransient<Surface.Mqtt.Mqtt>();
    //        services.AddTransient<Surface.Logger.Logger>();
    //        services.AddSingleton(typeof(IConfiguration), Configuration);

    //        services.AddSingleton<ILoggerFactory, LoggerFactory>();
    //        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
    //        services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

    //        var servicesProvider = services.BuildServiceProvider();

    //        _surfaceLogger = servicesProvider.GetRequiredService<Surface.Logger.Logger>();
    //        _mqtt = servicesProvider.GetRequiredService<Surface.Mqtt.Mqtt>();

    //        string str;

    //        str = new string('!', 100000000);
    //        _mqtt.SendMqttMessage("tosco", str + "@");

    //        return "0";
    //    }

    //    public static void CLPMonitorTesteCor()
    //    {
    //        Dictionary<string, string> cores = new Dictionary<string, string>();
    //        Dictionary<string, string> modelos = new Dictionary<string, string>();
    //        Dictionary<string, string> variaveis = new Dictionary<string, string>();
    //        const string START = "DB9.DBX2.4";

    //        string filename = @"/home/litma/testedoRafa.csv";
    //        var corAtual = "";
    //        var modeloAtual = "";
    //        var linha = "";

    //        cores.Add("DB9.DBX0.0", "branco ambiente");
    //        cores.Add("DB9.DBX0.1", "branco polar");
    //        cores.Add("DB9.DBX0.2", "vermelho tribal");
    //        cores.Add("DB9.DBX0.3", "cinza antique");
    //        cores.Add("DB9.DBX0.4", "preto carbon");
    //        cores.Add("DB9.DBX0.5", "preto shadow");
    //        cores.Add("DB9.DBX0.6", "vermelho colorado");
    //        cores.Add("DB9.DBX0.7", "billet silver");
    //        cores.Add("DB9.DBX1.0", "verde ricon");
    //        cores.Add("DB9.DBX1.1", "red velvet");
    //        cores.Add("DB9.DBX1.2", "deep brown");
    //        cores.Add("DB9.DBX1.3", "jazz blue");
    //        cores.Add("DB9.DBX1.4", "verde botanic");
    //        cores.Add("DB9.DBX1.5", "laranja aurora");
    //        cores.Add("DB9.DBX1.6", "teste pretos");
    //        cores.Add("DB9.DBX1.7", "teste brancos");

    //        variaveis.Add("DB2.DBW36", "valor R");
    //        variaveis.Add("DB2.DBW38", "valor G");
    //        variaveis.Add("DB2.DBW40", "valor B");
    //        variaveis.Add("DB2.DBW42", "valor I");
    //        variaveis.Add("DB2.DBW44", "r 8bit");
    //        variaveis.Add("DB2.DBW46", "g 8bit");
    //        variaveis.Add("DB2.DBW48", "b 8bit");
    //        variaveis.Add("DB2.DBW50", "i 8bit");
    //        variaveis.Add("DB2.DBW52", "valor banco Preto");
    //        variaveis.Add("DB2.DBW54", "valor banco Branco");

    //        modelos.Add("DB9.DBX2.0", "226");
    //        modelos.Add("DB9.DBX2.1", "521");
    //        modelos.Add("DB9.DBX2.2", "551 on");
    //        modelos.Add("DB9.DBX2.3", "551 off");

    //        S7.Net.Plc _plc = new Plc(CpuType.S71200, "192.168.1.140", 0, 1);
    //        _plc.Open();
    //        if (_plc.IsConnected)
    //        {
    //            _plc.Write(START, true);
    //            Console.WriteLine("Aguardando Testes");
    //            while (true)
    //            {
    //                filename = "";
    //                var start = _plc.Read(START);
    //                if (Convert.ToInt32(start) == 1)
    //                {
    //                    _plc.Write(START, false);
    //                    foreach (var cor in cores)
    //                    {
    //                        var read = _plc.Read(cor.Key);
    //                        if (Convert.ToInt32(read) == 1)
    //                        {
    //                            corAtual = cor.Value;
    //                            break;
    //                        }
    //                    }

    //                    foreach (var modelo in modelos)
    //                    {
    //                        var read = _plc.Read(modelo.Key);
    //                        if (Convert.ToInt32(read) == 1)
    //                        {
    //                            modeloAtual = modelo.Value;
                                
    //                            break;
    //                        }
    //                    }

    //                    filename = modeloAtual + ".csv";
    //                    Console.WriteLine(modeloAtual + " - " + corAtual);

    //                    if (File.Exists(filename) == false)
    //                    {
    //                        linha = "COR;";
    //                        foreach (var valor in variaveis)
    //                        {
    //                            linha += valor.Value + ";";
    //                        }
    //                        File.WriteAllText(filename, linha + "\r\n");
    //                    }

    //                    linha = corAtual + ";";
    //                    foreach (var valor in variaveis)
    //                    {
    //                        var read = _plc.Read(valor.Key);
    //                        linha += Convert.ToInt32(read) + ";";
    //                    }
    //                    File.AppendAllText(filename, linha + "\r\n");
    //                }
    //            }
    //        }
    //        _plc.Close();
    //    }

    //}

    //public class Station
    //{
    //    public int Version { get; set; }
    //}
}
