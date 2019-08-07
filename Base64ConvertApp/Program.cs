using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Base64ConvertApp {
    public class Program {
        /// <summary>
        /// Let's do this.
        /// </summary>
        private static void Main(string[] args) {
            Console.WriteLine("Base64 ConvertApp");
            Console.WriteLine();

            var inf = GetArgsValue(args, "in");
            var ouf = GetArgsValue(args, "out");
            var o64 = GetArgsSwitchBool(args, "o64");

            if (inf == null ||
                ouf == null) {

                Console.WriteLine(" -in  <file> - base 64 file to be read.");
                Console.WriteLine(" -out <file> - normal file to be written.");
                Console.WriteLine(" -o64 - read in-file and write base 64 to out-file.");

                return;
            }

            byte[] bytes;

            if (o64) {
                bytes = File.ReadAllBytes(inf);
                var text = Convert.ToBase64String(bytes, 0, bytes.Length);

                File.WriteAllText(
                    ouf,
                    text);
            }
            else {
                bytes = Convert.FromBase64String(
                    File.ReadAllText(inf));

                File.WriteAllBytes(
                    ouf,
                    bytes);
            }
        }

        /// <summary>
        /// Get value from the args array as parameter.
        /// </summary>
        private static string GetArgsValue(
            IReadOnlyList<string> args,
            string key) {

            var max = args.Count - 1;

            for (var i = 0; i < max; i++) {
                if (args[i] == "-" + key) {
                    return args[i + 1];
                }
            }

            return null;
        }

        /// <summary>
        /// Get boolean switch from args.
        /// </summary>
        private static bool GetArgsSwitchBool(
            IEnumerable<string> args,
            string key) {

            return args.Any(arg => arg == "-" + key);
        }
    }
}