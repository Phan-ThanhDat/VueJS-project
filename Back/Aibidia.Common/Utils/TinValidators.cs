using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.Common.Utils
{
    public static class TinValidator
    {
        public static bool IsFinnishTin(string tin)
        {
            //Tunnus on muotoa NNNNNNN-T.
            //Tunnuksen numeroita(7 kpl, tarvittaessa lisätään alkuun nolla; numeroita oli aikaisemmin kuusi,
            // ja tätä vanhaa muotoa voi hyvin harvoin nähdä vieläkin) painotetaan vasemmalta lähtien kertoimilla 7, 9, 10, 5, 8, 4 ja 2.
            //Tulot lasketaan yhteen.
            //Summa jaetaan 11:llä.
            //Jos jakojäännös on 0, tarkistusnumero on 0.
            //Ei anneta tunnuksia, jotka tuottaisivat jakojäännöksen 1.
            //Jos jakojäännös on 2..10, tarkistusnumero on 11 miinus jakojäännös.

            if (tin == null)
            {
                throw new ArgumentNullException(nameof(tin));
            }

            string[] parts = tin.Split('-');
            if (parts.Length != 2)
            {
                return false;
            }

            if (parts[0].Length < 6 || parts[0].Length > 7)
            {
                return false;
            }

            if (parts[0].Length == 6)
            {
                parts[0] = "0" + parts[0];
            }

            int checksum;
            if (!Int32.TryParse(parts[1], out checksum))
            {
                return false;
            }

            int[] weights = new int[] { 7, 9, 10, 5, 8, 4, 2 };
            int total = 0;
            for (int i = 0; i <= 6; i++)
            {
                int num;
                if (!Int32.TryParse(parts[0][i].ToString(), out num))
                {
                    return false;
                }
                total += num * weights[i];
            }

            int remainder = total % 11;
            int compare = 0;
            if (remainder >= 2 && remainder <= 10)
            {
                compare = 11 - remainder;
            }
            return compare == checksum;
        }
    }
}
