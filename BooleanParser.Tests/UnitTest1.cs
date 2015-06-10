using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooleanParser.Tests
{
	[TestClass]
	public class UnitTest1
	{
		private const string Query = "(((johnsons OR \"johnson s\") NEAR/2 baby) OR \"j j\" OR johnsonsbaby OR \"johnson johnson\") AND (nourish OR nourishment OR nourishing OR nourished OR crack OR cracks OR peels OR peeling OR peeled OR peel OR good OR bad OR amazing OR terrible OR shocking OR great OR better OR worse OR best OR worst OR beautiful OR appealing OR excellent OR \"first class\" OR pleasing OR splendid OR lush OR extraordinary OR superior OR styling OR horrible OR terrible OR atrocious OR weird OR wierd OR favourite OR lovely OR atrocious OR awful OR crap OR dreadful OR garbage OR inadequate OR inferior OR junk OR lousy OR poor OR stinks OR substandard OR important OR importance OR vital OR shit OR improve OR improved OR improvement OR improvements OR improving OR pointless OR quality OR horrid OR horrific OR nightmare OR ridiculous OR cool OR hot OR effective OR berkesan OR efektif OR works OR berkhasiat OR khasiat OR \"ada khasiat\" OR merekah OR rekahan OR menggelupas OR kelupasan OR baik OR \"tidak baik\" OR buruk OR hebat OR mengejutkan OR \"sangat baik\" OR \"lebih baik\" OR \"lebih buruk\" OR \"lebih tidak baik\" OR \"tak baik\" OR \"lebih tak baik\" OR bagus OR \"tidak bagus\" OR \"tak bagus\" OR \"lebih tidak bagus\" OR \"lebih tak bagus\" OR \"paling baik\" OR \"paling buruk\" OR \"paling tak baik\" OR \"paling tak bagus\" OR terbaik OR terbagus OR terburuk OR terhebat OR cantik OR memukau OR \"kelas satu\" OR \"1st klas\" OR menyenangkan OR \"luar biasa\" OR pelik OR \"sangat pelik\" OR jelek OR kesukaan OR \"lebih suka\" OR sampah OR \"tak cukup\" OR \"tak mencukupi\" OR \"tidak cukup\" OR \"tidak mencukupi\" OR busuk OR \"berbau busuk\" OR berbau OR \"tak elok\" OR \"tidak elok\" OR \"di bawah tahap kualiti\" OR \"bawah standard\" OR penting OR kepentingan OR mementingkan OR keutamaan OR mengutamakan OR kritikal OR kebaikan OR penambahbaikan OR menambahbaik OR \"menambah kebaikan\" OR \"menjadi lebih baik\" OR kualiti OR \"tak guna\" OR \"tidak guna\" OR \"tak berguna\" or \"tidak berguna\" OR menakutkan OR \"mimpi ngeri\" OR \"sangat menakutkan\" OR merepek OR kool OR berkesan OR keberkesanan) NOT (thesignatureoflove OR \"signature of love\" OR \"ernie johnson\" OR palette OR (\"ready to ship\" AND \"sizes available\") OR \"minimum order required\" OR \"medicaid expansion\" OR \"star trek\" OR \"j j green\" OR hardy OR kickson OR nba)";

		[TestMethod]
		public void TestMethod1()
		{
			var block = BooleanParser.ParseQuery(Query);

			foreach (var term in block)
			{
				Console.WriteLine("=== {0} ===", term.Key);
				foreach (var keyword in term.Value)
				{
					Console.WriteLine(keyword);
				}
			}
		}

		[TestMethod]
		public void TestMethod2()
		{
			var block = BooleanParser.ConvertQuery(Query);
			Console.WriteLine("========= Brand ==========");
			foreach (var keyword in block.Brands)
			{
				Console.WriteLine(keyword);
			}

			Console.WriteLine("========= Qualifiers ==========");
			foreach (var keyword in block.Qualifiers)
			{
				Console.WriteLine(keyword);
			}

			Console.WriteLine("========= Exclusions ==========");
			foreach (var keyword in block.Exclusions)
			{
				Console.WriteLine(keyword);
			}
		}
	}
}
