﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Personnages.Classes;

namespace TeamsMaker_METIER.Personnages
{
    public class Personnage
    {
        #region --- Attributs ---
        private Classe classe;      //Classe du personnage
        private int lvlPrincipal;   //Niveau dans son rôle principal
        private int lvlSecondaire;  //Niveau dans son rôle secondaire
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Classe du personnage
        /// </summary>
        public Classe Classe => this.classe;
        /// <summary>
        /// Rôle principal du personnage
        /// </summary>
        public Role RolePrincipal => this.classe.RolePrincipal();
        /// <summary>
        /// Rôle secondaire du personnage
        /// </summary>
        public Role RoleSecondaire => this.classe.RoleSecondaire();
        /// <summary>
        /// Le personnage a-t-il un rôle secondaire ?
        /// </summary>
        public bool AUnRoleSecondaire => this.RoleSecondaire != Role.AUCUN;
        /// <summary>
        /// Level dans son rôle principal
        /// </summary>
        public int LvlPrincipal => this.lvlPrincipal;
        /// <summary>
        /// Level dans son rôle secondaire
        /// </summary>
        public int LvlSecondaire => this.lvlSecondaire;
        #endregion

        #region --- Constructeurs ---
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classe">Classe du personnage</param>
        /// <param name="lvlPrincipal">Niveau dans son rôle principal</param>
        /// <param name="lvlSecondaire">Niveau dans son rôle secondaire</param>
        public Personnage(Classe classe, int lvlPrincipal, int lvlSecondaire)
        {
            this.classe = classe;
            this.lvlPrincipal = lvlPrincipal;
            this.lvlSecondaire = lvlSecondaire;
        }
        #endregion
    }
}
