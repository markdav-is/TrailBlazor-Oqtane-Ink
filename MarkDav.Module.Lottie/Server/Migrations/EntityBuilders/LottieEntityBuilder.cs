using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace MarkDav.Module.Lottie.Migrations.EntityBuilders
{
    public class LottieEntityBuilder : AuditableBaseEntityBuilder<LottieEntityBuilder>
    {
        private const string _entityTableName = "MarkDavLottie";
        private readonly PrimaryKey<LottieEntityBuilder> _primaryKey = new("PK_MarkDavLottie", x => x.LottieId);
        private readonly ForeignKey<LottieEntityBuilder> _moduleForeignKey = new("FK_MarkDavLottie_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public LottieEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override LottieEntityBuilder BuildTable(ColumnsBuilder table)
        {
            LottieId = AddAutoIncrementColumn(table,"LottieId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> LottieId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
