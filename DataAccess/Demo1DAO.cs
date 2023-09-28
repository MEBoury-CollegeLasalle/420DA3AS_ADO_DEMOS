using Microsoft.Data.SqlClient;
using System.Data;

namespace TestPatates.DataAccess;
internal class Demo1DAO {
    private readonly SqlConnection connection;
    private readonly SqlDataAdapter adapter;
    private readonly DataSet dataSet;

    private static readonly string TABLE_NAME = "Demo1";

    private static readonly string SELECT_QUERY =
        $"SELECT * FROM {TABLE_NAME};";

    private static readonly string INSERT_QUERY =
        $"INSERT INTO {TABLE_NAME} " +
        $"(Name, Description) " +
        $"VALUES (@nameParam, @descParam); " +
        $"SELECT * FROM {TABLE_NAME} WHERE Id = SCOPE_IDENTITY();";

    private static readonly string UPDATE_QUERY =
        $"UPDATE {TABLE_NAME} " +
        $"SET Name = @nameParam, " +
        $"Description = @descParam " +
        $"WHERE (" +
        $"Id = @idParam " +
        $"AND Name = @oldNameParam " +
        $"AND Description LIKE @oldDescParam" + // NOTE: comparer deux colonnes 'TEXT' ne marche pas avec '='
        $");";

    private static readonly string DELETE_QUERY =
        $"DELETE FROM {TABLE_NAME} WHERE Id = @idParam;";


    public Demo1DAO(DataSet dataSet) {
        this.connection = DatabaseConnectionService.GetConnection();
        this.dataSet = dataSet;
        this.adapter = this.CreateDataAdapter();
    }



    private SqlDataAdapter CreateDataAdapter() {
        // Création de l'adapter
        SqlDataAdapter adapter = new SqlDataAdapter();
        // Configuration en cas de schéma interne manquant (créer)
        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

        // création de la commande de sélection (select ALL)
        adapter.SelectCommand = new SqlCommand(SELECT_QUERY, this.connection);



        // création de la commande d'insertion
        adapter.InsertCommand = new SqlCommand(INSERT_QUERY, this.connection);

        // Configuration du remplacement de la ligne de la DataTable insérée
        // par la ligne retournée (SCOPE_IDENTITY trick). Fonctionne aussi
        // pour les colonnes a valeurs générées autres que la clé (DateCreated)
        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;

        // paramétrage de la commande d'insertion
        _ = adapter.InsertCommand.Parameters.Add("@nameParam", SqlDbType.NVarChar, 64, "Name");
        _ = adapter.InsertCommand.Parameters.Add("@descParam", SqlDbType.Text, -1, "Description");



        // création de la commande de modification
        adapter.UpdateCommand = new SqlCommand(UPDATE_QUERY, this.connection);

        // paramétrage de la commande de modification AVEC checks de concurrence
        _ = adapter.UpdateCommand.Parameters.Add("@nameParam", SqlDbType.NVarChar, 64, "Name");
        _ = adapter.UpdateCommand.Parameters.Add("@descParam", SqlDbType.Text, -1, "Description");
        _ = adapter.UpdateCommand.Parameters.Add("@idParam", SqlDbType.Int, 4, "Id");

        // paramètres de checks de concurrence: notez les paramètres ajoutés à la clause
        // WHERE pour valider si la ligne a été modifiée depuis le chargement.
        // Si la ligne a été modifiée dans la BdD entre le moment du loading de la table
        // et le moment de la sauvegarde des changements, la commande update retournera
        // 0 lignes affectées. Ceci causera le lancement d'une DBConcurrencyException.
        adapter.UpdateCommand.Parameters.Add("@oldNameParam", SqlDbType.NVarChar, 64, "Name")
            .SourceVersion = DataRowVersion.Original;
        adapter.UpdateCommand.Parameters.Add("@oldDescParam", SqlDbType.Text, -1, "Description")
            .SourceVersion = DataRowVersion.Original;



        // création et paramétrage de la commende de suppression
        adapter.DeleteCommand = new SqlCommand(DELETE_QUERY, this.connection);
        _ = adapter.DeleteCommand.Parameters.Add("@idParam", SqlDbType.Int, 4, "Id");

        return adapter;
    }


    public DataTable GetDataTable() {
        // re-chargement de la table.
        this.LoadData();
        return this.dataSet.Tables[TABLE_NAME] ?? throw new Exception("DataTable not found even after load.");
    }

    public void LoadData() {
        // ouvrir connexion
        if (this.connection.State != ConnectionState.Open) {
            this.connection.Open();
        }

        // Vider la table si elle existe
        if (this.dataSet.Tables.Contains(TABLE_NAME)) {
            this.dataSet.Tables[TABLE_NAME]?.Clear();
        }

        // remplir la table depuis la BdD
        _ = this.adapter.Fill(this.dataSet, TABLE_NAME);

        // fermer connexion
        this.connection.Close();


        // configuration de la table pour le gridview
        DataTable table = this.dataSet.Tables[TABLE_NAME] ?? throw new Exception("DataTable not found even after load.");

        // colonne ID est readonly et auto-increment
        DataColumn idColumn = table.Columns["Id"] ?? throw new Exception("Id DataColumn not found.");
        idColumn.AutoIncrement = true;
        idColumn.ReadOnly = true;

        // colonne 'Name' est non-nulle et max 64 caractères
        DataColumn nameColumn = table.Columns["Name"] ?? throw new Exception("Mane DataColumn not found.");
        nameColumn.AllowDBNull = false;
        nameColumn.MaxLength = 64;

        // colonne 'Description' accepte les NULLs
        DataColumn descColumn = table.Columns["Description"] ?? throw new Exception("Description DataColumn not found.");
        descColumn.AllowDBNull = true;

        // Colonne 'DateCreated' est générée par la base de donnée (accepte NULL + readonly)
        DataColumn dateCreatedColumn = table.Columns["DateCreated"] ?? throw new Exception("DateCreated DataColumn not found.");
        dateCreatedColumn.AllowDBNull = true;
        dateCreatedColumn.ReadOnly = true;
    }

    public void SaveChanges() {
        // ouverture de connexion
        if (this.connection.State != ConnectionState.Open) {
            this.connection.Open();
        }

        // sauvegarde des changements apportés
        _ = this.adapter.Update(this.dataSet, TABLE_NAME);

        // fermeture de connexion
        this.connection.Close();
    }

    public void CancelChanges() {
        // rejet (rollback interne) des changements
        DataTable? table = this.dataSet.Tables[TABLE_NAME];
        if (table != null) {
            table.RejectChanges();
        }
    }

}
