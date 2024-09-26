using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace MarkDav.Module.Ink.Migrations.EntityBuilders
{
    public class InkEntityBuilder : AuditableBaseEntityBuilder<InkEntityBuilder>
    {
        private const string _entityTableName = "MarkDavInk";
        private readonly PrimaryKey<InkEntityBuilder> _primaryKey = new("PK_MarkDavInk", x => x.InkId);
        private readonly ForeignKey<InkEntityBuilder> _moduleForeignKey = new("FK_MarkDavInk_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public InkEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override InkEntityBuilder BuildTable(ColumnsBuilder table)
        {
            InkId = AddAutoIncrementColumn(table,"InkId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> InkId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
