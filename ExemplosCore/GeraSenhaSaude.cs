using System;
using System.Collections.Generic;
using System.Text;

namespace ExemplosCore
{
    class GeraSenhaSaude
    {

        static string GeraSenha(string cnpj, int data, int modulo)
        {
            String senha = data.ToString("D6") + modulo.ToString("D2") + CalculaDV(cnpj);
            senha += CalculaDV(senha);

            int r1 = 7; // DateTime.Now.Millisecond % 10;
            int r2 = 1; // DateTime.Now.Millisecond % 10;

            int i = r1;
            string retorno = "";
            foreach (char c in senha)
            {
                int n = Convert.ToInt32(senha[i].ToString());
                n = r2 - n;
                if (n < 0) n = 10 + n;
                retorno += n.ToString();
                i++;
                if (i == 12) i = 0;
            }
            return retorno + "-" + r1 + r2;
        }

        static int VerificaSenha(string senha, string cnpj, int modulo)
        {
            string retorno = "";

            senha = ApenasNumeros(senha);
            int r1 = Convert.ToInt32(senha[senha.Length - 2].ToString());
            int r2 = Convert.ToInt32(senha[senha.Length - 1].ToString());

            senha = senha.Remove(12);

            int i = 12 - r1;
            if (i == 12) i = 0;
            foreach (char c in senha)
            {
                int n = Convert.ToInt32(senha[i].ToString());
                n = r2 - n;
                if (n < 0) n = n + 10;
                retorno += n.ToString();
                i++;
                if (i == 12) i = 0;
            }

            var dvEnviado = retorno.Substring(10);
            retorno = retorno.Remove(10);
            var dvCalculado = CalculaDV(retorno);

            if (dvEnviado != dvCalculado)
            {
                Console.WriteLine("Senha inválida: DV errado");
            }
            else
            {
                var dvCNPJEnviado = retorno.Substring(8, 2);
                retorno = retorno.Remove(8);
                var dvCNPJCalculado = CalculaDV(cnpj);
                if (dvCNPJCalculado != dvCNPJEnviado)
                {
                    Console.WriteLine("Senha não é desse CNPJ");
                }
                else
                {
                    int moduloCalculado = Convert.ToInt32(retorno.Substring(6, 2));
                    if (modulo != moduloCalculado)
                    {
                        Console.WriteLine($"Senha para o Módulo {moduloCalculado}, solicitado o Módulo {modulo}");
                    }
                    else
                    {
                        return Convert.ToInt32(retorno.Remove(6));
                    }
                }
            }
            return 0;
        }


        static string CalculaDV(String texto)
        {
            int dv = 0;
            texto = ApenasNumeros(texto);
            int i = 1;
            foreach (char c in texto)
            {
                dv += Convert.ToInt32(c.ToString()) + i;

                i++;
                if (i > 10) i = 1;
            }

            while (dv > 99)
            {
                var tmp = dv.ToString();
                dv = 0;
                foreach (char c in tmp)
                {
                    dv += Convert.ToInt32(c.ToString());
                }
            }

            return dv.ToString("D2");
        }

        static string ApenasNumeros(string texto)
        {
            string retorno = "";
            foreach (char c in texto)
            {
                if (Char.IsDigit(c))
                    retorno += c;
            }
            return retorno;
        }
    }
}
