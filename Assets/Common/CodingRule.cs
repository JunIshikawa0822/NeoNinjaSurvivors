//コーディングのルールを決めます。
//Determine coding rules.

//基本的にどんな名前も、用途や役割がわかりやすい名前にしてください。
//Any name should be easy to understand its use and role. 

//1.変数（フィールド、ローカル変数、グローバル変数）
//1.Variable(Field, Local Variable, Global Variable)
//
//変数はlowerCamelCaseで命名します
//Use lowerCamelCase for Variable.
//
//例
//good examples
//
//  int playerVelocity = 0;
//  float playerAttackAngle = 3.0f;
//  Rigidbody playerRigidBody;
//
//これはダメです
//bad examples
//
//  int speed
//  float value

//2.メソッド/関数
//2.Method, Function
//
//メソッド/関数はUpperCamelCaseで命名します。
//Use UpperCamelCase for Method or Function.
//
//例
//examples
//
//  void PlayerMove()
//  {
//
//  }
//
//  int CalculatePlayerVelocity()
//  {
//
//  }

//3.引数
//3.Parameter/Argument
//
//引数はlowerCamelCaseで命名し、また、引数の前には_をつけてください。
//Use lowerCamelCase for Parameter/Argument, and insert _ before its name.
//
//例
//examples
//
//  void PlayerMove(int _playerMoveDistance, Vector3 _mouseVector)
//  {
//     player.Transform.position = _playerMoveDistance * _mosueVector;
//  }
//
//  int CalculatePlayerVelocity(float _playerVelocity)
//  {
//     _playerVelocity = _playerVelocity * 10000000;
//  }

//4.配列/リスト
//4.Array/List
//
//配列/リストはlowerCamelCaseで命名し、また、名前の最後にArray/Listをつけてください。
//Use lowerCamelCase for Array/List, and put Array/List after its name.
//
//例
//examples
//
//  int[] enemyHPArray = new int[5];
//  List<Rigidbody> rigidBodyList = new List<Rigidbody>();

//他の人のためにも、メソッドの役割や処理の説明を残すようにしてください。"//"　スラッシュ2回でコメントアウトできます。
//Be sure to leave a description of the Variable/Method's role and processing for other members.
//You can comment out by //.






