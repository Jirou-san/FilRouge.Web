﻿using System;
using System.Collections.Generic;
using System.Linq;
using FilRouge.Model;
using FilRouge.Model.Entities;

namespace FilRouge.Services
{
    /// <summary>
    /// Services liés au quizz, pdf, gestion, mails, CRUD...
    /// </summary>
    public class QuizzService 
    {
        #region Properties

        #endregion
        /// <summary>
        /// Constructeur de la classe de QuizzService
        /// </summary>
        public QuizzService() { } //Constructeur
        #region Methods
        
        /// <summary>
        /// Méthode permettant d'obtenir un quizz en fonction de son id
        /// Fonctionne avec une fluentQuerry
        /// </summary>
        /// <param name="id">l'ID du quizz (sa clé primaire)</param>
        /// <returns>Retourne un objet Quizz</returns>
        public Quizz GetQuizz(int id)
        {
            Quizz fluentQuery = new Quizz();
            FilRougeDBContext db = new FilRougeDBContext();
            try
            {
                fluentQuery = db.Quizz.Single(e => e.QuizzId == id);
                if(fluentQuery == null)
                {
                    throw new WrongIdQuizz(id);
                }
                db.Dispose();                
            }
            catch(FormatException e)
            {
                db.Dispose();
                Console.WriteLine(e.Message );
            }
            return fluentQuery;
        }
        /// <summary>
        /// Fonction retournant tous les quizz dans une liste de Quizz
        /// Fonctionne avec une fluentQuerry
        /// </summary>
        /// <returns>Retourne tous les quizz</returns>
        public List<Quizz> GetAllQuizz()
        {
            List<Quizz> desQuizz = new List<Quizz>();
            FilRougeDBContext db = new FilRougeDBContext();
            try
            {   
                IQueryable<Quizz> fluentQuery = db.Quizz.Select(e => e);
                if(fluentQuery.Count() == 0)
                {
                    throw new ListQuizzEmpty("La liste des quizz est vide");
                }
                foreach (var item in fluentQuery)
                {
                    desQuizz.Add(item);
                }
                db.Dispose();
            }
            catch (ListQuizzEmpty)
            {
                db.Dispose();
            }
            
            return desQuizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionsQuizz"></param>
        /// <param name="lesQuestions"></param>
        /// <param name="questionlibre"></param>
        /// <param name="nombrequestions"></param>
        /// <returns></returns>
        ///
        
        public static List<Question> AddQuestionToQuizz(bool questionlibre, int nombrequestions, int technoid, int difficultymasterid)
        {
            Random rand = new Random();
            List<Question> sortedQuestionsQuizz = new List<Question>();

            FilRougeDBContext db = new FilRougeDBContext();
            try
            {
                //Requêtes Linq
                int nbrTotalQuestions = db.Question.Select(e => e).Count();
                IQueryable<Question> AllQuestionsByTechno = db.Question.Where(e => e.TechnologyId == technoid);
                IQueryable<TypeQuestion> TypesQuestions = db.TypeQuestion.Select(e => e); 

                //Génération de la liste de questions en fonction des paramètres
                foreach (var rate in RatesQuizz)
                {//Pour gérer la répartition des questions dans le quizz

                    for (int i = 0; i < Math.Floor(); i++)
                    {           
                        //pas encore utilisé
                            IQueryable<Question> QuestionByType = AllQuestionsByTechno.Where(e => e.TypeQuestion.NameType == "Question libre");
                        foreach (var question in QuestionByType)
                        {//Vérification par id de la présence d'une question
                            if (question.QuestionId == rand.Next(0, nbrTotalQuestions))
                            {
                                foreach (var newQuestion in sortedQuestionsQuizz)
                                {
                                    //Gestion des doublons
                                    if (question.QuestionId != newQuestion.QuestionId)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                db.Dispose();
            }
           

            return sortedQuestionsQuizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="difficultyid"></param>
        /// <param name="technoid"></param>
        /// <param name="userid"></param>
        /// <param name="nomuser"></param>
        /// <param name="prenomuser"></param>
        /// <param name="questionlibre"></param>
        /// <param name="nombrequestions"></param>
        public static void CreateQuizz(int userid, int difficultymasterid, int technoid,string nomuser, string prenomuser, bool questionlibre, int nombrequestions)
        {
            List<Question> questionsQuizz = AddQuestionToQuizz(questionlibre,nombrequestions, technoid, difficultymasterid);
            int timer = 0;
            FilRougeDBContext db = new FilRougeDBContext();
            try
            {
                Contact creatingQuizzContact = db.Contact.Single(e => e.UserId == userid);
                DifficultyMaster difficultyQuizz = db.DifficultyMaster.Single(e => e.DiffMasterId == difficultymasterid);
                Technology technoQuizz = db.Technology.Single(e => e.TechnoId == technoid);
                
                Quizz unQuizz = new Quizz
                {
                    ContactId = userid,
                    DifficultyMasterId = difficultymasterid,
                    TechnologyId = technoid,
                    Timer = timer,
                    PrenomUser = prenomuser,
                    NomUser = nomuser,
                    NombreQuestion = nombrequestions,
                    EtatQuizz = 0,
                    QuestionLibre = questionlibre,
                    Contact = creatingQuizzContact,
                    DifficultyMaster = difficultyQuizz,
                    Questions = questionsQuizz,
                    Technology = technoQuizz
                };
                db.SaveChanges();
                db.Dispose();
            }
            catch (AlreadyInTheQuestionsList e)
            {
                Console.WriteLine(e.Message);
                db.Dispose();
            }
            catch (NoQuestionsForYou e)
            {
                Console.WriteLine(e.Message);
                db.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                db.Dispose();
            }
        }
        #endregion
    }
}
