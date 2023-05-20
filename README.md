# Project Title

[![Build Status]](#GeneriTree)

A brief description of what the project does.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Contact](#contact)

## Installation

1. Clone the repository: `git clone https://github.com/AleXio-23/CustomTreeGenerator.git`
2. Take and put classes from cloned repository into project.


## Usage

    //1. When data locates in single class.
    
        //a. Create base class where your data locates.
             
                using System; 

                namespace GeneriTree
                {
                    public partial class SourceClass
                    { 
                        public int Source_Id { get; set; } 
                        public int Source_ParentId { get; set; } 
                        public string? Source_Name { get; set; } 
                        public bool Source_IsActive { get; set; } 
                    }
                }
        
        //b. Create target class for tree view. !NOTE! its importent to have target class Lits proerty of itself!
            
                using System; 

                namespace GeneriTree
                {
                    public partial class TargetClass
                    { 
                        public int Target_Id { get; set; } 
                        public int Target_ParentId { get; set; } 
                        public string? Target_Name { get; set; } 
                        public bool Target_IsActive { get; set; } 
                        public List<TargetClass> Children {get;set;}
                    }
                } 

            // When we generate tree based on this class, children of parents will be located in 
              
                    public List<TargetClass> Children {get;set;}
            //property

        //c. We have a list of data with SourceClass.
             
                List<SourceClass> sourceClassList = new();

        //d. We need to create Key-Value Dictionary for properties. If there are differently named ones in both class. for example:
           // We have Source_Id in SourceClass and  Target_Id in TargetClass, but we need Source_Id meaning for Target_Id

            //Lets create dictionary for Id and other props:

          
                var propsDictionary = new Dictionary<string, string>()
                    {
                        {"Source_Id", "Target_Id"},
                        {"Source_Name", "Target_Name" },
                        {"Source_IsActive", "Target_IsActive" }
                    };

        //e. After that call Convert method from GeneriTree:
           
                List<TargetClass> transformedSourceClass = (List<TargetClass>)DataTreeGenerator.TranformObject<SourceClass,TargetClass>(sourceClassList, propsDictionary);

        //f. Finally, when we have data which is suitable for our tree generator.

        
                var tree = GeneriTree.TreeBuilder.GenerateMultilevelTree.Generate<TargetClass>(
                    transformedSourceClass,
                    transformedSourceClass => transformedSourceClass.Target_Id,
                    transformedSourceClass => transformedSourceClass.Target_ParentId,
                    (transformedSourceClass, children) => transformedSourceClass.Children = children
                    );

            // In this code, function parameters are for:
            //- transformedSourceClass : list of target class for tree data
            //- transformedSourceClass => transformedSourceClass.Target_Id : Id accessor for our TargetClass to determin which is Id field in our class (may have different name)
            //- transformedSourceClass => transformedSourceClass.Target_ParentId: Parent Id accessor for identify property based on what our generator generates tree  (may have different name)
            //- (transformedSourceClass, children) => transformedSourceClass.Children = children: Identifies property of children (may have different name) to generate generations

            //We also have additional parameters like sortIndexAccessor and maxDepth
                //- sortIndexAccessor if we have sortIndex or we want our data to be sorted based on any property, our call will be like:
               
                    var tree = GeneriTree.TreeBuilder.GenerateMultilevelTree.Generate<TargetClass>(
                        transformedSourceClass,
                        transformedSourceClass => transformedSourceClass.Target_Id,
                        transformedSourceClass => transformedSourceClass.Target_ParentId,
                        (transformedSourceClass, children) => transformedSourceClass.Children = children,
                        transformedSourceClass => transformedSourceClass.Target_Id,
                        );
              
                //Now our generations will be sorted based on Id.
                /*- maxDepth with default value 100 makes class to generate 100 level of generations. If you want any specific level generation and not make code slower for now reason, just specify number*/
                    
                        var tree = GeneriTree.TreeBuilder.GenerateMultilevelTree.Generate<TargetClass>(
                            transformedSourceClass,
                            transformedSourceClass => transformedSourceClass.Target_Id,
                            transformedSourceClass => transformedSourceClass.Target_ParentId,
                            (transformedSourceClass, children) => transformedSourceClass.Children = children,
                            transformedSourceClass => transformedSourceClass.Target_Id,
                            maxDepth: 4
                            );
                    
    //2. When data locates in different classes. Everything will be same, just we will have SourceClass1, SourceClass2
        //a. We will transform each of them to 1 target class
            List<TargetClass> transformedSourceClass1 = (List<TargetClass>)DataTreeGenerator.TranformObject<SourceClass1,TargetClass>(sourceClassList1, propsDictionaryForSourceClass1);
            List<TargetClass> transformedSourceClass2 = (List<TargetClass>)DataTreeGenerator.TranformObject<SourceClass2,TargetClass>(sourceClassList2, propsDictionaryForSourceClass2);

            //after that union separated lists transformedSourceClass1.Add(transformedSourceClass2);

            //and then everything will be same as on 1.f ( Finally, when we have data which is suitable for our tree generator.) stage


## Features

- Makes it easy to create parent-child unlimited relation for tree view
- Makes it easy to make tree in both ways.
    1. If all data is in same table
    2. If data is from across different tables, helps to manage as single class list, then generate tree.

 

## Contact

For questions or feedback, you can reach out to us at [alekogabelashvili777@gmail.com](mailto:email@alekogabelashvili777@gmail.com).