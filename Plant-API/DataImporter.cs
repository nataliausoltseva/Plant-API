using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public class DataImporter
{
    private readonly Dictionary<string, string> API_HEADERS = new Dictionary<string, string> {
        { "id", "Id" },
        { "scientific_name", "ScientificName" },
        { "rank", "Rank" },
        { "genus", "Genus" },
        { "family", "Family" },
        { "year", "Year" },
        { "author", "Author" },
        { "bibliography", "Bibliography" },
        { "common_name", "CommonName" },
        { "family_common_name", "FamilyCommonName" },
        { "image_url", "ImageUrl" },
        { "flower_color", "FlowerColor" },
        { "flower_conspicuous", "SlowerConspicuous" },
        { "foliage_color", "FoliageColor" },
        { "foliage_texture", "FoliageTexture" },
        { "fruit_color", "FruitColor" },
        { "fruit_conspicuous", "FruitConspicuous" },
        { "fruit_months", "FruitMonths" },
        { "bloom_months", "BloomMonths" },
        { "ground_humidity", "GroundHumidity" },
        { "growth_form", "GrowthForm" },
        { "growth_habit", "GrowthHabit" },
        { "growth_months", "GrowthMonths" },
        { "growth_rate", "GrowthRate" },
        { "edible_part", "EdiblePart" },
        { "vegetable", "Vegetable" },
        { "edible", "Edible" },
        { "light", "Light" },
        { "soil_nutriments", "SoilNutriments" },
        { "soil_salinity", "SoilSalinity" },
        { "anaerobic_tolerance", "AnaerobicTolerance" },
        { "atmospheric_humidity", "AnaerobicHumidity" },
        { "average_height_cm", "AverageHeightCm" },
        { "maximum_height_cm", "MaximumHeightCm" },
        { "minimum_root_depth_cm", "MinimumRootDepthCm" },
        { "ph_maximum", "PhMaximum" },
        { "ph_minimum", "PhMinimum" },
        { "planting_days_to_harvest", "PlantingDaysToHarvest" },
        { "planting_description", "PlantingDescription" },
        { "planting_sowing_description", "PlantingSowingDescription" },
        { "planting_row_spacing_cm", "PlantingRowSpacingCm" },
        { "planting_spread_cm", "PlantingSpreadCm" },
        { "synonyms", "Synonyms" },
        { "distributions", "Distributions" },
        { "common_names", "CommonNames" },
        { "url_usda", "UrlUsda" },
        { "url_tropicos", "UrlTropicos" },
        { "url_tela_botanica", "UrlTelaBotanica" },
        { "url_powo", "UrlPowo" },
        { "url_plantnet", "UrlPlantnet" },
        { "url_gbif", "UrlGbif" },
        { "url_openfarm", "UrlOpenfarm" },
        { "url_catminat", "UrlCatminat" },
        { "url_wikipedia_en", "UrlWikipediaEn" },
    };

    public List<Species> ImportCsv()
    {
        List<Species> rowsLines = new List<Species>();
        using (var reader = new StreamReader(@"E:\Development\new-species.csv"))
        {
            Int32 counter = 0;
            string[] headers = [];

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line != null)
                {
                    if (counter == 0)
                    {
                        headers = line.Split('\t');
                    }
                    else
                    {
                        Int16 headerCounter = 0;
                        Species species = new Species();

                        Type type = species.GetType();

                        if (type != null)
                        {
                            var values = line.Split('\t');
                            foreach (var item in headers)
                            {                               
                                string value = API_HEADERS[item];
                                int index = API_HEADERS.Keys.ToList().IndexOf(item);
                                PropertyInfo? field = type.GetProperty(value);

                                if (field != null && index < values.Length)
                                {
                                    if (item == "id")
                                    {
                                        field.SetValue(species, rowsLines.Count + 1);
                                    }
                                    else
                                    {
                                        switch (field.PropertyType)
                                        {
                                            case Type t when field.PropertyType == typeof(String):
                                                field.SetValue(species, values[index].ToString());
                                                break;
                                            case Type t when field.PropertyType == typeof(Nullable<Boolean>):
                                                field.SetValue(species, values[index] != "" ? true : false);
                                                break;
                                            case Type t when field.PropertyType == typeof(Nullable<Int32>):
                                                field.SetValue(species, values[index] != "" ? Convert.ToInt32(values[index]) : null);
                                                break;
                                            case Type t when field.PropertyType == typeof(Nullable<Double>):
                                                field.SetValue(species, values[index] != "" ? Convert.ToDouble(values[index]) : null);
                                                break;
                                            default:
                                                var fieldValue = values[index];
                                                field.SetValue(species, fieldValue == "" ? null : fieldValue.ToString());
                                                break;
                                        }
                                    }


                                }
                                headerCounter++;
                            }
                        }
                        rowsLines.Add(species);
                    }
                    counter++;
                }
            }
        }
        return rowsLines;
    }
}