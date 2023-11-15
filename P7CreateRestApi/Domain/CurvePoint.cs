using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Domain
{
    public class CurvePoint
    {
        // TODO: Map columns in data table CURVEPOINT with corresponding fields

        public int Id { get; set; }

        public byte? CurveId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AsOfDate { get; set; }

        public double? Term { get; set; }

        public double? CurvePointValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }
    }


public static class CurvePointEndpoints
{
	public static void MapCurvePointEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/CurvePoint", async (LocalDbContext db) =>
        {
            return await db.CurvePointss.ToListAsync();
        })
        .WithName("GetAllCurvePoints")
        .Produces<List<CurvePoint>>(StatusCodes.Status200OK);

        routes.MapGet("/api/CurvePoint/{id}", async (int Id, LocalDbContext db) =>
        {
            return await db.CurvePointss.FindAsync(Id)
                is CurvePoint model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCurvePointById")
        .Produces<CurvePoint>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/CurvePoint/{id}", async (int Id, CurvePoint curvePoint, LocalDbContext db) =>
        {
            var foundModel = await db.CurvePointss.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(curvePoint);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCurvePoint")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/CurvePoint/", async (CurvePoint curvePoint, LocalDbContext db) =>
        {
            db.CurvePointss.Add(curvePoint);
            await db.SaveChangesAsync();
            return Results.Created($"/CurvePoints/{curvePoint.Id}", curvePoint);
        })
        .WithName("CreateCurvePoint")
        .Produces<CurvePoint>(StatusCodes.Status201Created);


        routes.MapDelete("/api/CurvePoint/{id}", async (int Id, LocalDbContext db) =>
        {
            if (await db.CurvePointss.FindAsync(Id) is CurvePoint curvePoint)
            {
                db.CurvePointss.Remove(curvePoint);
                await db.SaveChangesAsync();
                return Results.Ok(curvePoint);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCurvePoint")
        .Produces<CurvePoint>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
}
