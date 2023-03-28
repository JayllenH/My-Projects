#ifdef __APPLE__
#include <GLUT/glut.h> // include glut for Mac
#else
#include <GL/freeglut.h> //include glut for Windows
#endif

#include <math.h>
#include <iostream>

using namespace std;
/*
Jay'llen Hathman
IGME 309 E03
9/16/22
*/
// Window's width and height
int width, height;

// global parameters defining the circle
int vertNum = 100; // total number of vertices for the circle
float x = 0.0f, y = 1.5f; // center postion of the circle
float r = 1.0f; // circle's radius



void init(void)
{
	// initialize the size of the window
	width = 600;
	height = 600;
}

void display(void)
{
	glClearColor(1.0, 1.0, 1.0, 0.0);
	glClear(GL_COLOR_BUFFER_BIT);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();

	// draw a circle as the ball ...
	glColor3f(1.0f, 0.0f, 0.0f);

	glBegin(GL_LINE_LOOP);
	for (int i = 0; i < vertNum; i++)
	{
		float t = (float)i / vertNum * 2.0f * 3.14f;
		glVertex2f(x + r * cos(t), y + r * sin(t));
	}
	glEnd();
	

	glutSwapBuffers();
}


void update()
{

	glutPostRedisplay();
}

void reshape(int w, int h)
{
	width = w;
	height = h;

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(-5.0, 5.0, -5.0, 5.0);

	glViewport(0, 0, (GLsizei)width, (GLsizei)height);

	glutPostRedisplay();
}

void keyboard(unsigned char key, int x, int y)
{
	if (key == 27)
		exit(0);

	if (key == 61 && vertNum != 100)
	{
		vertNum++;
		cout << "# of vertices are " << vertNum<< "\n";
	}
	else if (key == 45 && vertNum != 3)
	{
		vertNum--;
		cout << "# of vertices are " << vertNum << "\n";

	}
		
}


int main(int argc, char* argv[])
{
	init();
	glutInit(&argc, argv);

	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);

	glutInitWindowSize((int)width, (int)height);

	// create the window with a title
	glutCreateWindow("Bouncing Ball");

	/* --- register callbacks with GLUT --- */
	glutReshapeFunc(reshape);
	glutDisplayFunc(display);
	glutKeyboardFunc(keyboard);
	glutIdleFunc(update);

	//start the glut main loop
	glutMainLoop();

	return 0;
}