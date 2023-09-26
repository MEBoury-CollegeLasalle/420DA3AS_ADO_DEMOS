using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPatates.DataAccess;
using TestPatates.Presentation;

namespace TestPatates.Business;
internal class Demo1Service {
    private readonly Demo1DAO demo1DAO;
    private readonly Demo1Window demo1View;

    public Demo1Service(DataSet dataset) {
        // DataSet commun reçu depuis la classe principale App passé au DAO
        this.demo1DAO = new Demo1DAO(dataset);

        // fenêtre spécifique pour la gestion des 'Demo1'
        // Notez le passage de 'this' en argument qui va permettre à la fenêtre
        // d'accéder aux méthodes de l'instance de Demo1Service
        this.demo1View = new Demo1Window(this);
    }

    public void OpenDemo1View() {
        // Fonction métier: gérer les 'Demo1'

        // ouvrir la fenêtre de gestion des Démo1 en mode modal.
        // Attends la fermeture de la fenêtre modale avant de continuer.
        this.demo1View.OpenWithTable(this.demo1DAO.GetDataTable());
    }

    public void SaveChanges() {
        try {
            // Enregistrer les changements
            this.demo1DAO.SaveChanges();

        } catch (DBConcurrencyException ex) {
            // Erreur de concurrence: la BdD a été changée entre le chargement des données
            // et la tentative de sauvegarde des changements.

            // Afficher message d'erreur avec numéro de ligne
            DataRow? row = ex.Row;
            int? rowId = ex.Row?.Field<int>("Id");
            _ = MessageBox.Show($"Erreur de concurrence détectée dans la base de données: ligne Id #{rowId}.");

            // Annuler les changements à CETTE ligne
            if (row != null) {
                row.RejectChanges();
                row.ClearErrors();
            }
        }
    }

    public bool CheckChangesBeforeClosing() {
        // Vérification si des changements ont été apportés
        if (this.demo1DAO.GetDataTable().GetChanges() is not null) {

            // si oui, vérifier si l'utilisateur veux les annuler (continuer)
            DialogResult dialogResult = MessageBox.Show("Des changements ont été apportés à la table et seront annulés si vous continuez. Voulez-vous vraiment continuer?", "Attention!", MessageBoxButtons.YesNo);
            
            if (dialogResult != DialogResult.Yes) {
                // Si l'utilisateur ne veut pas continuer, retourner FAUX
                return false;
            }

        }
        // Pas de changements dans la table OU si l'utilisateur veut continuer,
        // annulation (rollback interne) des changements et retourner vrai.
        this.demo1DAO.CancelChanges();
        return true;
    }



}
