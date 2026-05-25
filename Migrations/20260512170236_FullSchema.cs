using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LaykaSommelier.Api.Migrations
{
    /// <inheritdoc />
    public partial class FullSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "descriptor_categories",
                columns: table => new
                {
                    descriptor_category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descriptor_category_name = table.Column<string>(type: "text", nullable: false),
                    descriptor_category_color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptor_categories", x => x.descriptor_category_id);
                });

            migrationBuilder.CreateTable(
                name: "drinks",
                columns: table => new
                {
                    drink_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    drink_name = table.Column<string>(type: "text", nullable: false),
                    drink_type = table.Column<string>(type: "text", nullable: false),
                    drink_sub_type = table.Column<string>(type: "text", nullable: true),
                    drink_country = table.Column<string>(type: "text", nullable: true),
                    drink_producer = table.Column<string>(type: "text", nullable: true),
                    drink_aged = table.Column<int>(type: "integer", nullable: false),
                    drink_abv = table.Column<double>(type: "double precision", nullable: false),
                    drink_image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drinks", x => x.drink_id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employee_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_name = table.Column<string>(type: "text", nullable: false),
                    employee_password = table.Column<string>(type: "text", nullable: false),
                    employee_position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredient_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_name = table.Column<string>(type: "text", nullable: false),
                    ingredient_acidity = table.Column<double>(type: "double precision", nullable: false),
                    ingredient_sugar_level = table.Column<double>(type: "double precision", nullable: false),
                    ingredient_abv = table.Column<double>(type: "double precision", nullable: false),
                    ingredient_image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.ingredient_id);
                });

            migrationBuilder.CreateTable(
                name: "making_methods",
                columns: table => new
                {
                    making_method_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    making_method_name = table.Column<string>(type: "text", nullable: false),
                    making_method_dilution = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_making_methods", x => x.making_method_id);
                });

            migrationBuilder.CreateTable(
                name: "sources",
                columns: table => new
                {
                    source_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_name = table.Column<string>(type: "text", nullable: false),
                    source_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sources", x => x.source_id);
                });

            migrationBuilder.CreateTable(
                name: "descriptors",
                columns: table => new
                {
                    descriptor_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descriptor_name = table.Column<string>(type: "text", nullable: false),
                    descriptor_category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptors", x => x.descriptor_id);
                    table.ForeignKey(
                        name: "FK_descriptors_descriptor_categories_descriptor_category_id",
                        column: x => x.descriptor_category_id,
                        principalTable: "descriptor_categories",
                        principalColumn: "descriptor_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cocktails",
                columns: table => new
                {
                    cocktail_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cocktail_name = table.Column<string>(type: "text", nullable: false),
                    cocktail_volume = table.Column<double>(type: "double precision", nullable: false),
                    cocktail_acidity = table.Column<double>(type: "double precision", nullable: false),
                    cocktail_sugar_level = table.Column<double>(type: "double precision", nullable: false),
                    cocktail_abv = table.Column<double>(type: "double precision", nullable: false),
                    cocktail_glass = table.Column<string>(type: "text", nullable: false),
                    cocktail_making_method_id = table.Column<long>(type: "bigint", nullable: false),
                    cocktail_description = table.Column<string>(type: "text", nullable: false),
                    cocktail_author = table.Column<string>(type: "text", nullable: false),
                    cocktail_serving = table.Column<string>(type: "text", nullable: false),
                    cocktail_image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cocktails", x => x.cocktail_id);
                    table.ForeignKey(
                        name: "FK_cocktails_making_methods_cocktail_making_method_id",
                        column: x => x.cocktail_making_method_id,
                        principalTable: "making_methods",
                        principalColumn: "making_method_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reviewed_drink_id = table.Column<long>(type: "bigint", nullable: false),
                    review_source_id = table.Column<long>(type: "bigint", nullable: false),
                    review_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_reviews_drinks_reviewed_drink_id",
                        column: x => x.reviewed_drink_id,
                        principalTable: "drinks",
                        principalColumn: "drink_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_sources_review_source_id",
                        column: x => x.review_source_id,
                        principalTable: "sources",
                        principalColumn: "source_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingredients_descriptors",
                columns: table => new
                {
                    ingredient_id = table.Column<long>(type: "bigint", nullable: false),
                    descriptor_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients_descriptors", x => new { x.ingredient_id, x.descriptor_id });
                    table.ForeignKey(
                        name: "FK_ingredients_descriptors_descriptors_descriptor_id",
                        column: x => x.descriptor_id,
                        principalTable: "descriptors",
                        principalColumn: "descriptor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingredients_descriptors_ingredients_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "ingredient_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cocktails_ingredients",
                columns: table => new
                {
                    cocktail_id = table.Column<long>(type: "bigint", nullable: false),
                    ingredient_id = table.Column<long>(type: "bigint", nullable: false),
                    ingredient_volume = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cocktails_ingredients", x => new { x.cocktail_id, x.ingredient_id });
                    table.ForeignKey(
                        name: "FK_cocktails_ingredients_cocktails_cocktail_id",
                        column: x => x.cocktail_id,
                        principalTable: "cocktails",
                        principalColumn: "cocktail_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cocktails_ingredients_ingredients_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "ingredient_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suggestions",
                columns: table => new
                {
                    suggestion_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    suggested_cocktail_id = table.Column<long>(type: "bigint", nullable: false),
                    suggestion_employee_id = table.Column<long>(type: "bigint", nullable: false),
                    suggestion_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggestions", x => x.suggestion_id);
                    table.ForeignKey(
                        name: "FK_suggestions_cocktails_suggested_cocktail_id",
                        column: x => x.suggested_cocktail_id,
                        principalTable: "cocktails",
                        principalColumn: "cocktail_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suggestions_employees_suggestion_employee_id",
                        column: x => x.suggestion_employee_id,
                        principalTable: "employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "descriptors_reviews",
                columns: table => new
                {
                    descriptor_id = table.Column<long>(type: "bigint", nullable: false),
                    review_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptors_reviews", x => new { x.descriptor_id, x.review_id });
                    table.ForeignKey(
                        name: "FK_descriptors_reviews_descriptors_descriptor_id",
                        column: x => x.descriptor_id,
                        principalTable: "descriptors",
                        principalColumn: "descriptor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_descriptors_reviews_reviews_review_id",
                        column: x => x.review_id,
                        principalTable: "reviews",
                        principalColumn: "review_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cocktails_cocktail_making_method_id",
                table: "cocktails",
                column: "cocktail_making_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_cocktails_ingredients_ingredient_id",
                table: "cocktails_ingredients",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_descriptors_descriptor_category_id",
                table: "descriptors",
                column: "descriptor_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_descriptors_reviews_review_id",
                table: "descriptors_reviews",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_descriptors_descriptor_id",
                table: "ingredients_descriptors",
                column: "descriptor_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_review_source_id",
                table: "reviews",
                column: "review_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_reviewed_drink_id",
                table: "reviews",
                column: "reviewed_drink_id");

            migrationBuilder.CreateIndex(
                name: "IX_suggestions_suggested_cocktail_id",
                table: "suggestions",
                column: "suggested_cocktail_id");

            migrationBuilder.CreateIndex(
                name: "IX_suggestions_suggestion_employee_id",
                table: "suggestions",
                column: "suggestion_employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cocktails_ingredients");

            migrationBuilder.DropTable(
                name: "descriptors_reviews");

            migrationBuilder.DropTable(
                name: "ingredients_descriptors");

            migrationBuilder.DropTable(
                name: "suggestions");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "descriptors");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "cocktails");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "drinks");

            migrationBuilder.DropTable(
                name: "sources");

            migrationBuilder.DropTable(
                name: "descriptor_categories");

            migrationBuilder.DropTable(
                name: "making_methods");
        }
    }
}
