using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Functions;
using TraitBrowser.Models;
using TraitBrowserWebApp.Models;

namespace TraitBrowserWebApp.Data
{
    public static class XlsFilePath
    {
        public static string traitXlsFilePath = "Res\\CharacterTraits.xlsx";
        public static string BuffXlsFilePath = "Res\\Buffs.xlsx";
        public static string locBuffXlsFilePath = "Res\\LocBuff.xlsx";
    }
    public class SeedData
    {
        public static void InitializeTrait(IServiceProvider serviceProvider){
            using var context = new TraitBrowserContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TraitBrowserContext>>());

            if (context == null || context.Trait == null)
            {
                throw new NullReferenceException(
                    "Null TraitBrowserContext or Trait DbSet");
            }
            
            if (context.Trait.Any()){
                return;
            }
            
            var xlsFilePath = XlsFilePath.BuffXlsFilePath;
            var traits = GetSeedTraits(xlsFilePath);
            context.Trait.AddRange(traits);
            context.SaveChanges();
        }

        public static void InitializeLocBuff(IServiceProvider serviceProvider){
            using var context = new LocBuffContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LocBuffContext>>());

            if (context == null || context.LocBuff == null)
            {
                throw new NullReferenceException(
                    "Null LocBuffContext or LocBuff set.");
            }

            if (context.LocBuff.Any()){
                return;
            }

            var xlsFilePath = XlsFilePath.locBuffXlsFilePath;
            var locBuffs = GetSeedLocBuffs(xlsFilePath);
            context.LocBuff.AddRange(locBuffs);
            context.SaveChanges();
        }

        public static List<Trait> GetSeedTraits(string xlsFilePath){
            if (string.IsNullOrEmpty(xlsFilePath)){
                throw new ArgumentException("XLS file path is required");
            }
            if (!File.Exists(xlsFilePath)){
                throw new FileNotFoundException("XLS file not found");
            }

            var traits = new List<Trait>();
            var traitId = "";

            //TODO: Add seed traits
            using(var fs = new FileStream(xlsFilePath, FileMode.Open, FileAccess.Read)){
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);

                for(int rowIdx = sheet.FirstRowNum + 1; rowIdx < sheet.LastRowNum; rowIdx++){
                    var row = sheet.GetRow(rowIdx);
                    if (row == null){
                        continue;
                    }
                    
                    if (row.GetCell(0) != null){
                        if (row.GetCell(0).ToString() == "skip"){
                            continue;
                        }
                        if (row.GetCell(0).ToString() == "end"){
                            break;
                        }
                    }

                    if (row.GetCell(1) == null){
                        continue;
                    }
                    traitId = row.GetCell(1).ToString();
                    var tag = int.TryParse(traitId, out int tagValue);
                    
                    if (tag){
                        var trait = new Trait{
                            TraitId = tagValue,
                            Name = row.GetCell(2).ToString(),
                            Genre = "",
                            Description = "",
                            LocName = "",
                            locInfo = ""
                        };
                        
                        if (row.GetCell(24) != null)
                        {
                            trait.Genre = row.GetCell(24).ToString();
                            if (trait.Genre == "特征")
                            {
                                continue;
                            }
                        }
                        if (row.GetCell(21) != null)
                        {
                            trait.locInfo = row.GetCell(21).ToString();
                        }
                        traits.Add(trait);
                    }
                    
                    /*
                    if (tag){
                        var trait = new Trait{
                            TraitId = tagValue,
                            Name = row.GetCell(2).ToString(),
                            Genre = "特征",
                            Description = "",
                            LocName = "",
                            locInfo = ""
                        };
                        traits.Add(trait);
                    }
                    */
                }
            }
            return traits;
        }

        public static List<LocBuff> GetSeedLocBuffs(string xlsFilePath){
            if (string.IsNullOrEmpty(xlsFilePath)){
                throw new ArgumentException("XLS file path is required");
            }
            if (!File.Exists(xlsFilePath)){
                throw new FileNotFoundException("XLS file not found");
            }

            var locBuffs = new List<LocBuff>();

            using(var fs = new FileStream(xlsFilePath, FileMode.Open, FileAccess.Read)){
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);

                for(int rowIdx = sheet.FirstRowNum + 1; rowIdx < sheet.LastRowNum; rowIdx++){
                    var row = sheet.GetRow(rowIdx);
                    if (row == null){
                        continue;
                    }
                    
                    if (row.GetCell(0) != null){
                        if (row.GetCell(0).ToString() == "skip"){
                            continue;
                        }
                        if (row.GetCell(0).ToString() == "end"){
                            break;
                        }
                    }

                    var locBuff = new LocBuff{
                        Name = row.GetCell(2)!.ToString() ?? "",
                        SChinese = row.GetCell(3)!.ToString() ?? "",
                        TChinese = "",
                        English = ""
                    };

                    if (row.GetCell(4) != null){
                        locBuff.TChinese = row.GetCell(4).ToString();
                    }
                    if (row.GetCell(5) != null){
                        locBuff.English = row.GetCell(5).ToString();
                    }
                    
                    locBuffs.Add(locBuff);
                }
            }
            return locBuffs;
        }
    }
}