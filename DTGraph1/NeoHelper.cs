﻿namespace DTGraph1 {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Neo4jClient;
    using Neo4jClient.SchemaManager;

  

    public class NeoTestHelper
    {
        #region Public Methods and Operators

        public void BuildGraph(IGraphClient graphClient)
        {
           graphClient.Cypher.Create(this.GetTestGraphCreate()).ExecuteWithoutResults();
        }

        public void DeleteGraph(IGraphClient graphClient)
        {
            graphClient.Cypher.Match("(n)").OptionalMatch("(n)-[r]-()").Delete("r,n").ExecuteWithoutResults();
        }

        public string GetTestGraphCreate()
        {
            const string testGraph =
                @"(Stomatology : Frame {name:'Stomatology'})
CREATE (Anamnesis:Frame {name:'Anamnesis'})
CREATE (Examination:Frame {name:'Examination'})
CREATE (Dent:Frame {name:'Dent'})
CREATE (DataOfSubjectiveExamination:Frame {name:'Data Of Subjective Examination'})

CREATE (Pain:Frame {name:'Pain'})
CREATE (Complaints:Frame {name:'Complaints'})

CREATE (CosmeticalDefect:Slot {name:'Cosmetical Defect'})
CREATE (BreachOfForm:Term {name:'Breach of Form'})
CREATE (BreachOfColor:Term {name:'Breach of Color', colors:['light', 'dark', 'red']})
CREATE 
    (CosmeticalDefect)-[:HAS_TERM {index:0}]->(BreachOfForm),
    (CosmeticalDefect)-[:HAS_TERM {index:1}]->(BreachOfColor)


CREATE (DentalCavityPresented:Slot {name:'Dental Cavity Presented'})

CREATE (GeneralBreaches:Slot {name:'General Breaches'})
CREATE (Headache:Term {name:'Headache'})
CREATE (LackOfAppetite:Term {name:'Lack of Appetite'})
CREATE (SleepDisturbance:Term {name:'SleepDisturbance'})
CREATE (Fewer:Term {name:'Fewer'})
CREATE
    (GeneralBreaches)-[:HAS_TERM {index:0}]->(Headache),
    (GeneralBreaches)-[:HAS_TERM {index:1}]->(LackOfAppetite),
    (GeneralBreaches)-[:HAS_TERM {index:2}]->(SleepDisturbance),
    (GeneralBreaches)-[:HAS_TERM {index:3}]->(Fewer)

CREATE (AdditionalComplaints:Slot {name:'Additional Complaints'})
CREATE (FillingsDestroyed:Term {name:'Fillings Destroyed'})
CREATE (FillingsMoving:Term {name:'Fillings Moving'})
CREATE (FoodGetsStuckInTheTeeth:Term {name:'Food Gets Stuck In The Teeth'})
CREATE
    (AdditionalComplaints)-[:HAS_TERM {index:0}]->(FillingsDestroyed),
    (AdditionalComplaints)-[:HAS_TERM {index:1}]->(FillingsMoving),
    (AdditionalComplaints)-[:HAS_TERM {index:2}]->(FoodGetsStuckInTheTeeth)

CREATE
    (Complaints)-[:LINK_TO_FRAME{index:0}]->(Pain),
    (Complaints)-[:HAS_SLOT{index:1}]->(CosmeticalDefect),
    (Complaints)-[:HAS_SLOT{index:2}]->(DentalCavityPresented),
    (Complaints)-[:HAS_SLOT{index:3}]->(GeneralBreaches),
    (Complaints)-[:HAS_SLOT{index:4}]->(AdditionalComplaints)

CREATE
    (Stomatology)-[:LINK_TO_FRAME{index:0}]->(Anamnesis),
    (Stomatology)-[:LINK_TO_FRAME{index:1}]->(Examination)

CREATE
    (Examination)-[:LINK_TO_FRAME{index:0}]->(Dent)

CREATE
    (Dent)-[:LINK_TO_FRAME{index:0}]->(DataOfSubjectiveExamination)

CREATE
    (DataOfSubjectiveExamination)-[:LINK_TO_FRAME{index:0}]->(Complaints)
";
            return testGraph;
        }

        public void WriteAllIndexDescriptionsToConsole(IGraphClient graphClient)
        {
            List<IIndexMetadata> indexes = graphClient.ListAllIndexes();
            Console.WriteLine("\r\nThe Graph has the following Constraints and Indexes");
            foreach (IIndexMetadata indexDes in indexes)
            {
                Console.WriteLine(indexDes.ToString());
            }
            if (indexes.Count == 0)
            {
                Console.WriteLine("Nothing found");
            }
        }


        #endregion
    }
}