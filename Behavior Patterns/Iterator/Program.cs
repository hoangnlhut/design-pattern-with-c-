using System.Collections;

namespace Iterator
{
    #region BT1: source code in Gang Four of Refactoring code
    //abstract class Iterator : IEnumerator
    //{
    //    object IEnumerator.Current => Current();

    //    // Returns the key of the current element
    //    public abstract int Key();

    //    // Returns the current element
    //    public abstract object Current();

    //    // Move forward to next element
    //    public abstract bool MoveNext();

    //    // Rewinds the Iterator to the first element
    //    public abstract void Reset();
    //}

    //abstract class IteratorAggregate : IEnumerable
    //{
    //    // Returns an Iterator or another IteratorAggregate for the implementing
    //    // object.
    //    public abstract IEnumerator GetEnumerator();
    //}

    //// Concrete Iterators implement various traversal algorithms. These classes
    //// store the current traversal position at all times.
    //class AlphabeticalOrderIterator : Iterator
    //{
    //    private WordsCollection _collection;

    //    // Stores the current traversal position. An iterator may have a lot of
    //    // other fields for storing iteration state, especially when it is
    //    // supposed to work with a particular kind of collection.
    //    private int _position = -1;

    //    private bool _reverse = false;

    //    public AlphabeticalOrderIterator(WordsCollection collection, bool reverse = false)
    //    {
    //        this._collection = collection;
    //        this._reverse = reverse;

    //        if (reverse)
    //        {
    //            this._position = collection.getItems().Count;
    //        }
    //    }

    //    public override object Current()
    //    {
    //        return this._collection.getItems()[_position];
    //    }

    //    public override int Key()
    //    {
    //        return this._position;
    //    }

    //    public override bool MoveNext()
    //    {
    //        int updatedPosition = this._position + (this._reverse ? -1 : 1);

    //        if (updatedPosition >= 0 && updatedPosition < this._collection.getItems().Count)
    //        {
    //            this._position = updatedPosition;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    public override void Reset()
    //    {
    //        this._position = this._reverse ? this._collection.getItems().Count - 1 : 0;
    //    }
    //}

    //// Concrete Collections provide one or several methods for retrieving fresh
    //// iterator instances, compatible with the collection class.
    //class WordsCollection : IteratorAggregate
    //{
    //    List<string> _collection = new List<string>();

    //    bool _direction = false;

    //    public void ReverseDirection()
    //    {
    //        _direction = !_direction;
    //    }

    //    public List<string> getItems()
    //    {
    //        return _collection;
    //    }

    //    public void AddItem(string item)
    //    {
    //        this._collection.Add(item);
    //    }

    //    public override IEnumerator GetEnumerator()
    //    {
    //        return new AlphabeticalOrderIterator(this, _direction);
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // The client code may or may not know about the Concrete Iterator
    //        // or Collection classes, depending on the level of indirection you
    //        // want to keep in your program.
    //        var collection = new WordsCollection();
    //        collection.AddItem("First");
    //        collection.AddItem("Second");
    //        collection.AddItem("Third");

    //        Console.WriteLine("Straight traversal:");

    //        foreach (var element in collection)
    //        {
    //            Console.WriteLine(element);
    //        }

    //        Console.WriteLine("\nReverse traversal:");

    //        collection.ReverseDirection();

    //        foreach (var element in collection)
    //        {
    //            Console.WriteLine(element);
    //        }
    //    }
    //}
    #endregion

    #region BT2: source code in Pluralsight, iterator List Collection of person by name
    public class Person
    {
        public string  Name { get; set; }
        public string Country { get; set; }
        public Person(string name, string country)
        {
            Name = name;
            Country = country;
        }
    }

    /// <summary>
    /// Iterator Interface
    /// </summary>
    public interface IPeopleIterator
    {
        Person First();
        Person Next();
        bool IsDone { get; }
        Person CurrentItem { get; }
    }

    /// <summary>
    /// Aggregation
    /// </summary>
    public interface IPeopleCollection
    {
        IPeopleIterator CreatIterator();
    }


    /// <summary>
    /// Concrete Aggregation of List Colection
    /// </summary>
    public class PeopleCollection : List<Person>, IPeopleCollection
    {
        public IPeopleIterator CreatIterator()
        {
            return new PeopleIterator(this);
        }
    }

    /// <summary>
    /// Concrete Iterator
    /// </summary>
    public class PeopleIterator : IPeopleIterator
    {
        private PeopleCollection _peopleCollection;
        private int _current = 0;

        public PeopleIterator(PeopleCollection peopleCollection)
        {
            _peopleCollection = peopleCollection;
        }

        public int CurrentIndex => _current;

        public bool IsDone => _current >= _peopleCollection.Count;

        public Person CurrentItem => _peopleCollection.OrderBy(p => p.Name).ToList()[_current];

        public Person First()
        {
            _current = 0;
            return _peopleCollection.OrderBy(p => p.Name).ToList()[_current];
        }

        public Person Next()
        {
            _current++;
            if (!IsDone)
            {
                return _peopleCollection.OrderBy(p => p.Name).ToList()[_current];
            }
            else
            {
                return null;
            }
        }
    }

    // if you have any new collection, just add new class concrete iterator and new class concrete collection which are implement their interface
    // add new arrays collection below
    public class PeopleArrayCollection : ArrayList, IPeopleCollection
    {
        public IPeopleIterator CreatIterator()
        {
            return new PeopleArrayIterator(this);
        }
    }

    public class PeopleArrayIterator : IPeopleIterator
    {
        private PeopleArrayCollection _peopleArrayCollection;
        private int _current = 0;

        public PeopleArrayIterator(PeopleArrayCollection peopleCollection)
        {
            _peopleArrayCollection = peopleCollection;
        }

        public bool IsDone => _current >= _peopleArrayCollection.Count;

        public Person CurrentItem => (Person)_peopleArrayCollection.ToArray().OrderBy(p =>
        {
            var cast = (Person)p;
            return cast.Name;
        }).ToList()[_current]!;

        public Person First()
        {
            _current = 0;
            return (Person)_peopleArrayCollection.ToArray().OrderBy(p =>
            {
                var cast = (Person)p;
                return cast.Name;
            }).ToList()[_current]!;
        }

        public Person Next()
        {
            _current++;
            if (!IsDone)
            {
                return (Person)_peopleArrayCollection.ToArray().OrderBy(p =>
                {
                    var cast = (Person)p;
                    return cast.Name;
                }).ToList()[_current]!;
            }
            else
            {
                return null;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //create collection of list 
            //PeopleCollection people = new();
            //people.Add(new Person("Hoang", "VN"));
            //people.Add(new Person("Trang", "VN"));
            //people.Add(new Person("Viet", "VN"));
            //people.Add(new Person("Huy", "VN"));

            ////create Iterator
            //var peopleIterator = people.CreatIterator();

            //for (Person person = peopleIterator.First(); !peopleIterator.IsDone; person = peopleIterator.Next())
            //{
            //    Console.WriteLine(person.Name);
            //}

            //create collection of array
            PeopleArrayCollection array = new();
            array.Add(new Person("Hoang", "VN"));
            array.Add(new Person("Trang", "VN"));
            array.Add(new Person("Viet", "VN"));
            array.Add(new Person("Huy", "VN"));

            var arrayIterator = array.CreatIterator();

            for (Person person = arrayIterator.First(); !arrayIterator.IsDone; person = arrayIterator.Next())
            {
                Console.WriteLine(person.Name);
            }
        }
    }
    #endregion
}