using Adapter.PersonalInformation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Adapter.Renderers
{
    public class EvaluationRendererAdapter : IEvaluationRendererAdapter
    {
        private DataRenderer dataRenderer;

        public (string, int) ListTopicsAndScores(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            var adapter = new EvaluationDbAdapter(questionsAndAnswers);
            dataRenderer = new DataRenderer(adapter);

            var writer = new StringWriter();
            dataRenderer.Render(writer);

            return (writer.ToString(), GetScore(questionsAndAnswers));
        }

        private int GetScore(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            var score = 0;
            foreach (var qa in questionsAndAnswers)
            {
                if (qa.AnswerGivenShortForm.Equals(qa.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    score++;
                }
            }

            return score;
        }
    }

    internal class EvaluationDbAdapter : IDbDataAdapter
    {
        private readonly IEnumerable<QuestionAndAnswer> questionsAndAnswers;

        public EvaluationDbAdapter(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            this.questionsAndAnswers = questionsAndAnswers;
        }

        public int Fill(DataSet dataSet)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Topic", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Correct Answer", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Answer Given", typeof(string)));

            foreach (var qa in questionsAndAnswers)
            {
                var row = dataTable.NewRow();
                row[0] = qa.Topic;
                row[1] = qa.CorrectAnswer;
                row[2] = qa.AnswerGivenShortForm;
                dataTable.Rows.Add(row);
            }
            dataSet.Tables.Add(dataTable);
            dataSet.AcceptChanges();

            return dataTable.Rows.Count;
        }

        #region Not Implemented
        public IDbCommand DeleteCommand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IDbCommand InsertCommand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IDbCommand SelectCommand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IDbCommand UpdateCommand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public MissingMappingAction MissingMappingAction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public MissingSchemaAction MissingSchemaAction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public ITableMappingCollection TableMappings => throw new System.NotImplementedException();

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            throw new System.NotImplementedException();
        }

        public IDataParameter[] GetFillParameters()
        {
            throw new System.NotImplementedException();
        }

        public int Update(DataSet dataSet)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
