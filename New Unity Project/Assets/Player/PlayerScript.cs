
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Приватные переменные
    private Rigidbody2D rb;
    private bool FacingRight = true;
    private float HorizontalMove = 0f;
    private Animator anim;

    //Публичные переменные
    [Header("Player Movement Settings")]
    [Range(0, 10f)] public float speed = 1f;
    [Range(0, 15f)] public float jumpForce = 8f;
    [Space]
    [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [Range(-5f, 5f)] public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;

    void Start()
    {
        //Присваиваем переменной rb компонент Rigidbody2D который висит на персонаже
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isJumping", true);
        }
        else 
        {
            if (isGrounded == true) 
            {
                anim.SetBool("isJumping", false);
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Импульс вверх умножая на силу прыжка
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        //Переменная со значением горизонтали (лево = -1; на месте = 0 ; право = 1;)
        HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        //Условие для вызова функции поворота персонажа
        if (HorizontalMove < 0 && FacingRight)
        {
            Flip();
        }
        else if (HorizontalMove > 0 && !FacingRight)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        //Настраиваем скорость движения по горизонтали а по вертикали оставляем также
        Vector2 targetVelocity = new Vector2(HorizontalMove * 10f, rb.velocity.y);
        rb.velocity = targetVelocity;

        CheckGround();
    }

    //Функция переворота спрайта персонажа
    private void Flip()
    {
        //Переключение переменной при повороте
        FacingRight = !FacingRight;

        //Изменение Scale при повороте. Текущий Scale умножаем на -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Функция для проверки - стоит ли персонаж на поверхности
    private void CheckGround()
    {
        //Создаём массив из коллайдеров = создаём окружность которая будет проверять столкновение с другими коллайдерами. 
        //(новый Вектор2 (X = Позиция игрока по X, Y = Позиция игрока по Y + Отступ от центра спрайта игрока по вертикали), радиус окружности)
        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);

        //Если в массиве colliders более одного коллайдера, то игрок на земле
        //Более одного потому-что 1 коллайдер висит на игроке и тоже попадает в зону видимости окружности! 
        if (colliders.Length > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}