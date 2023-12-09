﻿using System;
using System.IO;

namespace MyApp// Note: actual namespace depends on the project name.
{
    internal class Program
    {
         static void Main(string[] args)
        {
            string filePath = "datos.bin";

            EscribirDatosAleatorios(filePath);

            LeerDatos(filePath);

            Console.ReadLine();
            
        }

        static void EscribirDatosAleatorios(string filePath)
        {
            Random random = new Random();

            
            using(FileStream fileStream = new FileStream(filePath,FileMode.Create, FileAccess.Write))
            using(BinaryWriter writer = new BinaryWriter(fileStream))
            {
                EscribeDatosPosicion(writer, 0, GenerarNumeroAleatorio(random));
                EscribeDatosPosicion(writer, 1, GenerarNumeroAleatorio(random));
                EscribeDatosPosicion(writer, 2, GenerarNumeroAleatorio(random));

                Console.WriteLine("\nDatos escritos en el arhivo.");
            } 
        }

        static int GenerarNumeroAleatorio(Random random)
        {
            return random.Next(256);
        }

        static void EscribeDatosPosicion(BinaryWriter writer, int posicion, int dato)
        {
            long bytePosicion = posicion * sizeof(int);

            writer.Seek((int)bytePosicion, SeekOrigin.Begin);

            writer.Write(dato);
        }

        static void LeerDatos(string filePath)
        {
            using (FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read))
            using  (BinaryReader reader = new BinaryReader(fileStream))
            {
                Console.Write("Dato en posición 0: " + LeeDatosPosicion(reader, 0));
                Console.Write("\nDato en posición 1: " + LeeDatosPosicion(reader, 1));
                Console.Write("\nDato en posición 2: " + LeeDatosPosicion(reader, 2));
            }
        }

        static int LeeDatosPosicion(BinaryReader reader, int posicion)
        {
            long bytePosicion = posicion * sizeof(int);

            reader.BaseStream.Seek(bytePosicion, SeekOrigin.Begin);

            return reader.ReadInt32();
        }
    }
}

