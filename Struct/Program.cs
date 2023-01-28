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

DateTime InputDateTime(string message)
{
	bool inputResult;
	DateTime dt;

	do
	{

		Console.WriteLine(message);
		inputResult = DateTime.TryParse(Console.ReadLine(), out dt);

	} while (!inputResult);
	return dt;
}

bool InputBool(string message)
{
	bool inputResult;
	bool b;

	do
	{

		Console.WriteLine(message);
		inputResult = bool.TryParse(Console.ReadLine(), out b);

	} while (!inputResult);
	return b;
}

string InputString(string message)
{
	Console.WriteLine(message);
	return Console.ReadLine();
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
DateTime GetMinDeliveryDate(Product[] products) // поиск минимальной даты
{
	DateTime minDate = products[0].DeliveryDate;

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].DeliveryDate < minDate)
		{
			minDate = products[i].DeliveryDate;
		}
	}
	return minDate;
}

DateTime GetMaxDeliveryDate(Product[] products) // поиск максимальной даты
{
	DateTime maxDate = products[0].DeliveryDate;

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].DeliveryDate > maxDate)
		{
			maxDate = products[i].DeliveryDate;
		}
	}
	return maxDate;
}


int GetMinSelfLifeDays(Product[] products) // поиск минимальной срокагоднасти
{
	int minSelfLifeDays = products[0].SelfLifeDays;

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].SelfLifeDays < minSelfLifeDays)
		{
			minSelfLifeDays = products[i].SelfLifeDays;
		}
	}
	return minSelfLifeDays;
}

int GetMaxSelfLifeDays(Product[] products) // поиск максимальной срокагоднасти
{
	int maxSelfLifeDays = products[0].SelfLifeDays;

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].SelfLifeDays > maxSelfLifeDays)
		{
			maxSelfLifeDays = products[i].SelfLifeDays;
		}
	}
	return maxSelfLifeDays;
}


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

Product CreateProduct(Product[] products, ref int CURRENT_ID, bool isNewId)
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


	product.Name = InputString("Введите название продукта: ");

	product.Contractor = InputString("Введит  поставщика: ");

	product.DeliveryDate = InputDateTime("Введите дату доставки: ");

	product.SelfLifeDays = InputInt("Введите срок годности: ");

	product.Balance = InputInt("Введите остаток продукта: ");

	return product;
}

Product CreateEmptyProduct()
{
	Product product;
	product.Id = 0;
	product.Name = "";
	product.Contractor = "";
	product.Balance = 0;
	product.SelfLifeDays = 0;
	product.DeliveryDate = DateTime.Now;
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

#region Retrive Methods
bool FindProductById(Product[] products, int id, out Product product)
{
	int indexPrint = GetIndexById(products, id);

	if (indexPrint == -1)
	{
		product = CreateEmptyProduct();
		return false;
	}
	else
	{
		product = products[indexPrint];
		return true;
	}
}

Product[] FindProductsFromMinToMaxDeliveryDate(Product[] products, DateTime minData, DateTime maxData)
{
	Product[] findedProducts = null;

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].DeliveryDate >= minData && products[i].DeliveryDate <= maxData)
		{
			AddNewProduct(ref findedProducts, products[i]);
		}
	}
	return findedProducts;
}



Product[] FindProductsFromMinToMaxSelfLifeDays(Product[] products, int minSelfLifeDays, int maxSelfLifeDays)
{
	Product[] findedProducts = null;

	for (int i = 0; i < products.Length; i++)
	{
		if (products[i].SelfLifeDays >= minSelfLifeDays && products[i].SelfLifeDays <= maxSelfLifeDays)
		{
			AddNewProduct(ref findedProducts, products[i]);
		}
	}
	return findedProducts;
}

#endregion

#region Sort Methods
void SortproductsByBalance(Product[] products, bool asc) // сортировка пузырьком
{
	Product[] temp;
	bool sort;
	int offset = 0;

	do
	{
		sort = true;

		for (int i = 0; i < products.Length - 1 - offset; i++)
		{
			bool compareResult;

			if (asc)
			{
				compareResult = products[i + 1].Balance < products[i].Balance;
			}
			else
			{
				compareResult = products[i + 1].Balance > products[i].Balance;
			}

			if (compareResult)
			{
					temp = products;
					products[i] = products[i + 1];
					products = temp;
			

				sort = false;
			}
		}

		offset++;
	} while (!sort);
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
	Console.WriteLine("6. Find product by id");
	Console.WriteLine("7. Find products from min to max delivery date");
	Console.WriteLine("8. Find products from min to max Self Life Days date");
	Console.WriteLine("9. Sort products by balance");
	Console.WriteLine("0. Exit");
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
		case 6:
			{
				int id = InputInt("InputInt id for retrive:");
				Product prodact;
				bool isFinded = FindProductById(products, id, out Product product);

				if (isFinded)
				{
					PrintProduct(product);
				}
				else
				{
					Console.WriteLine("Print is impossibe. Element not found");
				}
			}
			break;
		case 7:
			{
				Console.WriteLine($"input min end max delivery date from {GetMinDeliveryDate(products).ToShortDateString()} to {GetMaxDeliveryDate(products).ToShortDateString()}");
				Console.WriteLine("min date: ");
				DateTime minDate = DateTime.Parse(Console.ReadLine());

				Console.WriteLine("max date: ");
				DateTime maxDate = DateTime.Parse(Console.ReadLine());

				Product[] findedProducts = FindProductsFromMinToMaxDeliveryDate(products, minDate, maxDate);

				PrintManyProducts(findedProducts);
			}
			break;

		case 8:
			{
				Console.WriteLine($"input min end max Self Life Days date from {GetMinSelfLifeDays(products)} to {GetMaxSelfLifeDays(products)}");
				Console.WriteLine("min  Self Life Days: ");
				int minSelfLifeDays = int.Parse(Console.ReadLine());

				Console.WriteLine("max  Self Life Days: ");
				int maxSelfLifeDays = int.Parse(Console.ReadLine());

				Product[] findedProducts = FindProductsFromMinToMaxSelfLifeDays(products, minSelfLifeDays, maxSelfLifeDays);

				PrintManyProducts(findedProducts);
			}
			break;
		case 9:
			Console.WriteLine("Input asc desc sort (trueor false)");  // сщртировка бузырьковая от меньшего к большему и на обород
			bool asc = bool.Parse(Console.ReadLine());
			SortproductsByBalance(products, asc);
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