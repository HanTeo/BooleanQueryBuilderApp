using System;
using System.Collections.Generic;
using DataAccess;

namespace BooleanParser
{
	public enum TermType
	{
		Brand,
		Qualifier,
		Exclusion
	}

	public class BooleanParser
	{
		public static IEnumerable<HumanQuery> LoadFromExcelFile(string path)
		{
			var dt = DataTable.New.ReadExcel(path);

			foreach (var row in dt.Rows)
			{
				yield return ConvertQuery(row.Values[0]);
			}			
		}
		
		public static HumanQuery ConvertQuery(string query)
		{
			var results = ParseQuery(query);
			var humanQuery = new HumanQuery();

			foreach (var keyValuePair in results)
			{
				switch (keyValuePair.Key)
				{
					case TermType.Brand:
						humanQuery.Brands = keyValuePair.Value;
						break;
					case TermType.Qualifier:
						humanQuery.Qualifiers = keyValuePair.Value;
						break;
					case TermType.Exclusion:
						humanQuery.Exclusions = keyValuePair.Value;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return humanQuery;
		}

		public static IEnumerable<KeyValuePair<TermType, IEnumerable<string>>> ParseQuery(string query)
		{
			var type = TermType.Brand;
			var terms = MakeTerms(query);
			foreach (var term in terms)
			{
				if (term == " AND ") 
				{
					type = TermType.Qualifier;
					continue;
				}

				if(term == " NOT ")
				{
					type = TermType.Exclusion;
					continue;
				}

				var innerTerms = new List<string>();
				foreach (var inner in MakeInnerTerms(term))
				{
					innerTerms.Add(inner[0] != '(' ? inner.Trim(new[] {'"'}) : inner);
				}
				yield return new KeyValuePair<TermType, IEnumerable<string>>(type, innerTerms);
			}
		}

		private static IEnumerable<string> MakeInnerTerms(string query)
		{
			string trimmed;

			if (query[0] == '(' && query[query.Length - 1] == ')')
			{
				var front = query.Remove(0, 1);

				trimmed = front.Remove(front.Length - 1);
			}
			else
			{
				throw new ArgumentException("Query term does not have correct accompanying brackets");
			}

			var terms = new List<string>();

			var brackets = 0;
			string buffer = String.Empty;

			foreach (var character in trimmed)
			{
				switch (character)
				{
					case '(':
						if (brackets == 0 && buffer != String.Empty)
						{
							terms.AddRange(buffer.Split(new[]{" OR "}, StringSplitOptions.RemoveEmptyEntries));
							buffer = String.Empty;
						}
						brackets++;
						buffer += character;
						break;

					case ')':
						brackets--;
						buffer += character;
						if (brackets == 0)
						{
							terms.Add(buffer);
							buffer = String.Empty;
						}
						break;

					default:
						if (brackets < 0)
							throw new ArgumentOutOfRangeException("Cannot find matching closing bracket");

						if (brackets >= 0)
							buffer += character;

						break;
				}
			}

			if(!String.IsNullOrWhiteSpace(buffer))
				terms.AddRange(buffer.Split(new []{" OR "}, StringSplitOptions.RemoveEmptyEntries));

			return terms;
		}

		private static IEnumerable<string> MakeTerms(string query)
		{
			var terms = new List<string>();

			var brackets = 0;
			string buffer = String.Empty;

			foreach (var character in query)
			{
				switch (character)
				{
					case '(':
						if (brackets == 0 && buffer != String.Empty)
						{
							terms.Add(buffer);
							buffer = String.Empty;
						}

						brackets++;
						buffer += character;
						break;

					case ')':
						brackets--;
						buffer += character;

						if (brackets == 0)
						{
							terms.Add(buffer);
							buffer = String.Empty;
						}

						if (brackets < 0)
							throw new ArgumentOutOfRangeException("Cannot find matching bracket");
						break;

					default:
						if (brackets < 0)
							throw new ArgumentOutOfRangeException("Cannot find matching bracket");

						if (brackets >= 0)
							buffer += character;

						break;
				}
			}
			return terms;
		}
	}
}
