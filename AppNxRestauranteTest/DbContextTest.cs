using AppNxRestaurante.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppNxRestauranteTest
{
    public class DbContextTest
    {

        DbContextTest()
        {
            initContext();
        }
        public static DbContextTest instance = null;

        public static DbContextTest GetInstance()
        {
            if (instance == null)
            {
                instance = new DbContextTest();
            }
            return instance;
        }


        public static DbRestauranteContext dbRestauranteTestContext = null;

        public void initContext()
        {
            var options = new DbContextOptionsBuilder<DbRestauranteContext>()
             .UseInMemoryDatabase(databaseName: "Test_NxRestaurante").Options;
            // Set up a context (connection to the "DB") for writing
            using (var _context = new DbRestauranteContext(options))
            {
                _context.TipoIdentificacion.AddAsync(new Entities.TipoIdentificacion { descripcion = "CC" });
                _context.TipoIdentificacion.AddAsync(new Entities.TipoIdentificacion { descripcion = "TI" });
                _context.TipoIdentificacion.AddAsync(new Entities.TipoIdentificacion { descripcion = "CE" });
                _context.SaveChangesAsync();

                _context.Usuario.AddAsync(new Entities.Usuario
                {
                    identidad = "1017143560",
                    nombre = "Milker",
                    apellido = "Sanchez",
                    contrasenia = "123456",
                    email = "milker_16@hotmail.com",
                    tipo = new Entities.TipoIdentificacion { id = 1, descripcion = "CC" }
                });
                _context.SaveChangesAsync();

                dbCarvajalTestContext = _context;
            }


        }

    }
}
}
