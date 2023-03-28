
#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/freeglut.h>
#endif

#include <iostream>
#include <vector>
#include <vector>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <glm/gtx/constants.hpp>

#include "Poly.h"
using namespace glm;
using namespace std;

float canvasSize[] = { 10.0f, 10.0f };
int rasterSize[] = { 800, 600 };

float color[3];

PolyObject* current_poly;
vector<PolyObject*> completed_polys;

float mousePos[2];

void init(void)
{
    mousePos[0] = mousePos[1] = 0.0f;
    color[0] = 1.0f;
    color[1] = color[2] = 0.0f;

	current_poly = new PolyObject();
	current_poly->setColor(vec3(color[0], color[1], color[2]));
}

void drawCursor()
{
    glColor3f(1.0f, 0.0f, 1.0f);
    glPointSize(10.0f);
    glBegin(GL_POINTS);
    glVertex2fv(mousePos);
    glEnd();
    glPointSize(1.0f);
}

void display(void)
{
    glClearColor(1.0, 1.0, 1.0, 0.0);
    glClear(GL_COLOR_BUFFER_BIT);

    glColor3fv(color);
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();

	// draw the completed polys
	for (int i = 0; i < completed_polys.size(); i++)
	{
		completed_polys[i]->draw();
	}
	// draw the current poly
	vec2 v = vec2(mousePos[0], mousePos[1]);
	current_poly->draw(v);

    drawCursor();
    glutSwapBuffers();
}

void reshape(int w, int h)
{
    rasterSize[0] = w;
    rasterSize[1] = h;

    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluOrtho2D(0.0, canvasSize[0], 0.0, canvasSize[1]);
    glViewport(0, 0, rasterSize[0], rasterSize[1]);

    glutPostRedisplay();
}

void mouse(int button, int state, int x, int y)
{
    if (button == GLUT_LEFT_BUTTON && state == GLUT_DOWN) {

        mousePos[0] = (float)x / rasterSize[0] * canvasSize[0];
        mousePos[1] = (float)(rasterSize[1] - y) / rasterSize[1] * canvasSize[1];

		vec2 v = vec2(mousePos[0], mousePos[1]);
		current_poly->addVertex(v);
        glutPostRedisplay();
    }
}

void motion(int x, int y)
{
    // mouse events are handled by OS, eventually. When using mouse in the raster window, it assumes top-left is the origin.
    // Note: the raster window created by GLUT assumes bottom-left is the origin.
    mousePos[0] = (float)x / rasterSize[0] * canvasSize[0];
    mousePos[1] = (float)(rasterSize[1] - y) / rasterSize[1] * canvasSize[1];

    glutPostRedisplay();
}

void keyboard(unsigned char key, int x, int y)
{
    switch (key) {
    case 27:
        exit(0);
        break;
	case ' ':
		completed_polys.push_back(current_poly);
		current_poly = new PolyObject();
		current_poly->setColor(vec3(color[0], color[1], color[2]));
		break;
    }
	
}

void menu(int value)
{
    switch (value) {
    case 0: // clear
		completed_polys.clear();
		current_poly->clearVertices();

        glutPostRedisplay();
        break;
    case 1: //exit
        exit(0);
    case 2: // red
        color[0] = 1.0f;
        color[1] = 0.0f;
        color[2] = 0.0f;

		current_poly->setColor(vec3(color[0], color[1], color[2]));

        glutPostRedisplay();
        break;
    case 3: // green
        color[0] = 0.0f;
        color[1] = 1.0f;
        color[2] = 0.0f;

		current_poly->setColor(vec3(color[0], color[1], color[2]));

        glutPostRedisplay();
        break;
    case 4: // blue
        color[0] = 0.0f;
        color[1] = 0.0f;
        color[2] = 1.0f;

		current_poly->setColor(vec3(color[0], color[1], color[2]));

        glutPostRedisplay();
        break;
    default:
        break;
    }
}
void createMenu()
{
    int colorMenu = glutCreateMenu(menu);
    glutAddMenuEntry("Red", 2);
    glutAddMenuEntry("Green", 3);
    glutAddMenuEntry("Blue", 4);

    glutCreateMenu(menu);
    glutAddMenuEntry("Clear", 0);
    glutAddSubMenu("Colors", colorMenu);
    glutAddMenuEntry("Exit", 1);
    glutAttachMenu(GLUT_RIGHT_BUTTON);
}

int main(int argc, char* argv[])
{
    init();
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
    glutInitWindowSize(rasterSize[0], rasterSize[1]);
    glutCreateWindow("Mouse Event - draw a triangle");

    glutReshapeFunc(reshape);
    glutDisplayFunc(display);
    glutKeyboardFunc(keyboard);
    glutMouseFunc(mouse);
    glutPassiveMotionFunc(motion);
    createMenu();
    glutMainLoop();
    return 0;


}