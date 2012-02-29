using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HomeworkSubmission.DAL
{
    public class TopicDAL : DAO
    {
        /// <summary>
        /// Gets all Topics
        /// </summary>
        /// <returns>Collection of Topics</returns>
        public static IEnumerable<Topic> GetAll()
        {
            return db.Topics;
        }

        /// <summary>
        /// Gets Topic by ID.
        /// </summary>
        /// <param name="topicID">The topic's ID.</param>
        /// <returns>The topice with the same ID</returns>
        public static Topic GetByID(int topiceID)
        {
            Topic topic = (from c in db.Topics
                           where c.ID == topiceID
                           select c).
            FirstOrDefault();

            return topic;
        }

        public static IEnumerable<Topic> GetAllActive(IEnumerable<Topic> topics)
        {
            return topics.Where(x => x.IsActive == true);
        }

        /// <summary>
        /// Gets all topics by course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        public static IEnumerable<Topic> GetAllByCourse(Cours course)
        {
            return course.Topics;
        }




        public static string GetNameByID(int ID)
        {
            return db.Topics.FirstOrDefault(x => x.ID == ID).Name;
        }

        /// <summary>
        /// Creates new Topic
        /// </summary>
        /// <param name="name">Topic name</param>
        /// <param name="isActive">Topic state</param>
        public static void Create(string name, Cours course, bool isActive)
        {
            Topic c = new Topic();
            c.Cours = course;
            c.Name = name;
            c.IsActive = isActive;

            db.AddToTopics(c);
        }

        /// <summary>
        /// Adds to course.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="course">The course.</param>
        public static void AddToCourse(Topic topic, Cours course)
        {
            topic.Cours = course;
            db.SaveChanges();
        }

        /// <summary>
        /// Updates the specified Topic.
        /// </summary>
        /// <param name="Topic">The Topice.</param>
        /// <param name="name">Topice's Name</param>
        /// <param name="isActive">The Topice's state</param>
        public static void Update(Topic topic, string name, bool isActive, DateTime activeFrom, DateTime activeTo)
        {
            topic.Name = name;
            topic.ActiveFrom = activeFrom;
            topic.ActiveTo = activeTo;
            topic.IsActive = isActive;
            db.SaveChanges();
        }

        /// <summary>
        /// Removes the Topic by ID.
        /// </summary>
        /// <param name="TopicID">The Topic ID.</param>
        public static void RemoveByID(int topicId)
        {
            object topicForDeletion;

            EntityKey topicKey = new EntityKey("WebCalendarEntities.Topics", "ID", topicId);

            if (db.TryGetObjectByKey(topicKey, out topicForDeletion))
            {
                try
                {
                    db.DeleteObject(topicForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The Topice with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        topicKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The Topice with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that Topic exists.\n",
                    topicKey.EntityKeyValues[0].Value));
            }
        }
    }
}