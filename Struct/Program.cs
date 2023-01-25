using Store;


int CURRENT_ID = 0;


#region System Methonds // Динамически рассширяюшийся массив
void ResizeArray(ref Product[] products, int newLength) // ref Product[] products ссылка указываюшая на массив, int newLength и колличество нового
{
	int minLength = newLength > products.Length ? products.Length : newLength; // условие узнать минимальную длинну ( скопировать столько элементо всколько поместиться в массив) если из большего перегоняем в меньшее newLength > products.Length то получаеться (?)мин.,длина products.Length иначе ( : ) поместяться те чем ограничены newLength.

	Product[] newArray = new Product[newLength]; // Создали массив длинной newLength

	for (int i = 0; i < minLength; i++) // все элименты нужно скопировать в новый элимент
	{
		newArray[i] = products[i]; // все элементы из массива products[i] перегоняем в newArray[i]
	}
	products = newArray; // для расширения для старого массива
}
#endregion

#region CRUD Methonds
void AddNewProduct(ref Product[] products, Product product) // рассширяем массив и в массив встовляем Product product
{
	if (products == null) // проверка на пустой массив
	{
		products = new Product[1]; // создали массив на 1 элемент
	}
	else
	{
		ResizeArray(ref products, products.Length + 1);// рассширяем массив длинной + 1
	}
	products[products.Length - 1] = product;// вычисляем последний элемент массива
}

void DeleteProductById(ref Product[] products, int id) // удаление по ID
{
	int indexDelete = GetIndexById(products, id);
	if (indexDelete == -1)
	{
		Console.WriteLine("Delete is impossible. Element not found");
		return; // выбивает процидуру
	}

	Product[] newProducts = new Product[products.Length - 1]; // новый массив создали
	int newI = 0;

	for (int i = 0; i < products.Length; i++)
	{
		if (i != indexDelete)
		{
			newProducts[newI] = products[i];
			newI++;
		}
	}
	products = newProducts; // присвоили старому массиву новый массив
}

void ClearAllProducts(ref Product[] products) // очистка массива прировняли к null
{
	products = null;
}

void UpdateProductById(Product[] products, int id, Product product)
{
	int indexUpdate = GetIndexById(products, id); // понячть на каком элементе лежит id что бы замение его новым
	if (indexUpdate == -1)
	{
		Console.WriteLine("Update is impossible. Element not found");
		return; // выбивает процидуру
	}

	product.Id = products[indexUpdate].Id; // при замене id сохраняем элемента когорого обновляем
	products[indexUpdate] = product; // переписали по индексу массив
}

void InsertproductIntoPosition(ref Product[] products, int position, Product product) // выполняем вставку
{
	if (products == null)
	{
		Console.WriteLine("Insert is impossible. Array is empty");
		return;
	}

	if (position < 1 || position > products.Length)
	{
		Console.WriteLine("Insert is impossible. Position not found");
		return;
	}
	int indexInsert = position - 1;

	Product[] newProducts = new Product[products.Length + 1]; // увеличили массив на 1 
	int oldI = 0;

	for (int i = 0; i < newProducts.Length; i++)
	{
		if (i != indexInsert)
		{
			newProducts[i] = products[oldI];
			oldI++;
		}
		else
		{
			newProducts[i] = product;
		}
	}
	products = newProducts;
}
#endregion

#region Tools Methods .// Заполнение таблицы и Распичатка ее.

int GetIndexById(Product[] products, int id) // ищем индексы чтоо бы совпали
{
	if (products == null) // проверка при удалении когда массив пустой если пустой тогда -1 не сушествует
	{
		return -1;
	}

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].Id == id)
		{
			return i;
		}
	}
	return -1;
}

static Product CreateProduct(Product[] products, ref int CURRENT_ID, bool isNewId)
{
	Product product;
	if (isNewId)
	{
		CURRENT_ID++;
		product.Id = CURRENT_ID;
	}
	else
	{
		product.Id = 0;
	}

	Console.WriteLine("Введите название продукта: ");
	product.Name = Console.ReadLine();

	Console.WriteLine("Введит  поставщика: ");
	product.Contractor = Console.ReadLine();

	Console.WriteLine("Введите дату доставки: ");
	product.DeliveryDate = DateTime.Parse(Console.ReadLine());

	Console.WriteLine("Введите срок годности: ");
	product.SelfLifeDays = int.Parse(Console.ReadLine());

	Console.WriteLine("Введите остаток продукта: ");
	product.Balance = int.Parse(Console.ReadLine());

	return product;
}
void PrintProduct(Product product)
{
	Console.WriteLine("{0, -3} {1, -15} {2, -15} {3, -12} {4, -4} {5, -4}", product.Id, product.Name, product.Contractor, product.DeliveryDate.ToShortDateString(), product.SelfLifeDays, product.Balance);
}

void PrintManyProducts(Product[] products)
{
	Console.WriteLine("{0, -3} {1, -15} {2, -15} {3, -12} {4, -4} {5, -4}", "ИД", "Название", "Поставщик", "Дата дост.", "СГ", "Ост");

	if (products == null)
	{
		Console.WriteLine("Array is empty");
	}
	else if (products.Length == 0)
	{
		Console.WriteLine("Array is empty");
	}
	else
	{
		for (int i = 0; i < products.Length; i++)
		{
			PrintProduct(products[i]);
		}

	}
	Console.WriteLine("------------------");
}
#endregion

#region Interface Methods // Управление меню
void PrintMenu()
{
	Console.WriteLine("1. Add new product");
	Console.WriteLine("2. Delete product by id");
	Console.WriteLine("3. Clear all products");
	Console.WriteLine("4. Update product by id");
	Console.WriteLine("5. Insert product into position");
	Console.WriteLine("0. Exit");
}

int InputInt(string message)
{
	bool inputResult;
	int number;

	do
	{

		Console.WriteLine(message);
		inputResult = int.TryParse(Console.ReadLine(), out number);

	} while (!inputResult);
	return number;
}
#endregion


Product[] products = null; // массив пустой

bool runProgram = true;
while (runProgram)
{
	Console.Clear();
	PrintManyProducts(products);

	PrintMenu();
	int menuPoint = InputInt("Input menu point: ");

	switch (menuPoint) // Описание действий меню
	{
		case 1:
			{
				Product product = CreateProduct(products, ref CURRENT_ID, true); // при нажатии 1 создаеться пункт меню
				AddNewProduct(ref products, product);

			}
			break;
		case 2:
			{
				int id = InputInt("Input id for delete: ");
				DeleteProductById(ref products, id);
			}
			break;
		case 3:
			{
				ClearAllProducts(ref products); // очистили массив
			}
			break;
		case 4:
			{
				int id = InputInt("Input id for update: "); // обновили элемент
				Product product = CreateProduct(products, ref CURRENT_ID, false);
				UpdateProductById(products, id, product);
			}
			break;
		case 5:
			{
				int position = InputInt("Input position for insert: ");
				Product product = CreateProduct(products, ref CURRENT_ID, true);
				InsertproductIntoPosition(ref products, position, product);
			}
			break;

		case 0:
			{
				Console.WriteLine("Program will be finish"); // выход из меню
				runProgram = false;
			}
			break;

		default:
			{
				Console.WriteLine("Unknown command"); // не известная команда
			}
			break;
	}
	Console.ReadKey();
}