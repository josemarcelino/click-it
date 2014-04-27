using UnityEngine;
using System.Collections;


public class LevelGenerator : MonoBehaviour {
	

	public Transform light; // OBJECTO BOLA VERDE
	public static int actual_level = 1;
	public static int incremento_inicial = 3;
 	private static int block_to_construct =actual_level + incremento_inicial;
	private float timer_new_light;// NOVO OBJECTO VERDE
	private float timer_new_light_blink;
	private float timer_new_ready_level; // SPAM DE NIVEL
	private float timer_level_up; // TIMER ENTRE NIVEIS
	// ATENÇAO MUDAR NO AUTO_DESTROYER O TIMER_CD TAMBEM
	private float timer_cd = 0.4f; // CD DE BOLAS VERDES
	private float timer_cd_read_click = 0.1f; // CD PARA LER A JOGADA DO JOGADOR
	private float level_cd = 1; // CD PARA PASSAR DE NIVEL
	private bool timer_ready = true; // VERIFICADOR DE NOVA BOLA VERDE
	private bool timer_ready_blink = true;
	private bool timer_ready_read_click = true; // VERIFICADOR DE NOVA JOGADA
	private bool timer_level_up_bool = false; // VERIFICADOR DE LEVEL UP
	private bool release_level = true;// VERIFICADOR DE COMECAR A MANDAR BOLAS
	private static Vector3 [] tab_Posicoes = new Vector3 [50]; // TAB DE POSICOES DAS BOLAS VERDES NO NIVEL ACTUAL
	public bool game_over = false;

	private int actual_input_record = block_to_construct - 1;


	void Start(){
		for(int i = 0; i < 50; i++)
		{
			tab_Posicoes[i] =new Vector3(0,0,0); 
			
		}
		timer_level_up = Time.time + level_cd;
		}

	
	void FixedUpdate(){
				if (timer_ready == false) {
						if (Time.time >= timer_new_light)
								timer_ready = true;
				}



				if (timer_ready_blink == false) {
					if (Time.time >= timer_new_light_blink)
				timer_ready_blink = true;
				}

				if (timer_ready_read_click == false) {
					if (Time.time >= timer_new_ready_level)
					timer_ready_read_click = true;
				}

				if (timer_level_up_bool == false) {
					if(Time.time >= timer_level_up)
					timer_level_up_bool = true;

				}
				if (block_to_construct == 0) {
					release_level = false;
					
				}
	}

	void Level_Generator(){

		if (release_level == true && timer_level_up_bool == true) {
			if(timer_ready == true && block_to_construct > 0)
			{
				block_to_construct--;
				int x = Random.Range (0, 4);
				int y = Random.Range (0, 4);
				Instantiate (light, new Vector3 (x, y, -1), Quaternion.identity);
				tab_Posicoes[block_to_construct] = new Vector3(x,y,0);
				timer_new_light = Time.time + timer_cd;
				timer_ready = false;
				
				
			}
		}

	}


	void Player_Input(){

				if (release_level == false && actual_input_record >= 0) {
				
					 if(Input.GetMouseButtonDown(1) && timer_ready_read_click == true)
					{
						RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition),Vector3.forward); 

				if (hit.transform != null && hit.transform.position == tab_Posicoes[actual_input_record]) { // POSICAO DO OBJECTO NO QUAL O CLICK ACERTOU
								
								Instantiate (light, tab_Posicoes[actual_input_record], Quaternion.identity);
								actual_input_record--;
								timer_new_ready_level = Time.time + timer_cd_read_click;
								timer_ready_read_click = false;

						}
						
						else if (hit.transform != null) {

								release_level = false;
								game_over = true;

						}
					}
				}
				else if (release_level == false){
				
					release_level = true;
					print (actual_level);
					actual_level++;

					block_to_construct = actual_level + incremento_inicial;
					actual_input_record = block_to_construct -1;


					timer_level_up = Time.time + level_cd;
					timer_level_up_bool = false;

		}
		

				
				

		}


	void GG(){
		if (timer_ready_blink == true) {
						for (int i = 0; i < 4; i++) {
								for (int j = 0; j < 4; j++) {
				
										Instantiate (light, new Vector3 (i, j, 0), Quaternion.identity);
								}
						}

			timer_ready_blink = false;
			timer_new_light_blink = Time.time +0.4f;
				}




		}
	// Update is called once per frame
	void Update () {

		if (game_over == false) {
						Level_Generator ();
						Player_Input ();
				}
		else {
			GG ();

		}

	}
}