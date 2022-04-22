﻿using FluentMigrator;
using NzbDrone.Core.Datastore.Migration.Framework;

namespace NzbDrone.Core.Datastore.Migration
{
    [Migration(66)]
    public class add_tags : NzbDroneMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Create.TableForModel("Tags")
                  .WithColumn("Label").AsString().NotNullable();

            Alter.Table("Series")
                 .AddColumn("Tags").AsString().Nullable();

            Alter.Table("Notifications")
                 .AddColumn("Tags").AsString().Nullable();

            IfDatabase("sqlite").Execute.Sql("UPDATE Series SET Tags = '[]'");
            IfDatabase("sqlite").Execute.Sql("UPDATE Notifications SET Tags = '[]'");
        }
    }
}
