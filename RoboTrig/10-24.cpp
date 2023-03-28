#ifdef __APPLE__
#include <GLUT/glut.h> // include glut for Mac
#else
#include <GL/freeglut.h> //include glut for Windows
#endif
//Jayllen H
//You Use W and S to select the Body Part you want to control and the use A and D to rotate the selected body part

// the window's width and height
int width, height;

float left = -2.0f;
float right = 2.0f;
float top = 1.5f;
float bottom = -1.5f;

bool showCoordinate = true;

float curTime;
float preTime;
float g_angle = 0.0f;

float rotations[16];

int curID = 0;

void drawQuad(int _id) {
	if (_id == curID)
		glColor3f(1.0f, 0.0f, 0.0f);
	else
		glColor3f(0.0f, 0.0f, 0.0f);
    glBegin(GL_LINE_LOOP);
    glVertex2f(left, bottom);
    glVertex2f(left, top);
    glVertex2f(right, top);
    glVertex2f(right, bottom);
    glEnd();
}

void drawCoordinates() {
    if (showCoordinate == false)
        return;

    glPushMatrix();
    glLineWidth(1.0f);
    glPointSize(5.0f);

    // draw x axis
    glColor3f(1.0f, 1.0f, 0.0f);
    glBegin(GL_LINES);
    glVertex2f(0.0f, 0);
    glVertex2f(10.0f, 0);
    glEnd();

    // draw y axis
    glColor3f(0.0f, 1.0f, 0.0f);
    glBegin(GL_LINES);
    glVertex2f(0, 0.0f);
    glVertex2f(0.0f, 10.0f);
    glEnd();

    // draw the original point
    glColor3f(0.0f, 0.0f, 0.0f);
    glBegin(GL_POINTS);
    glVertex2f(0, 0);
    glEnd();

    glPopMatrix();
}

void init(void)
{
    // initialize the size of the window
    width = 600;
    height = 600;

    curTime = preTime = 0.0f;
}

// called when the GL context need to be rendered
void display(void)
{
    // clear the screen to white, which is the background color
    glClearColor(1.0, 1.0, 1.0, 0.0);

    // clear the buffer stored for drawing
    glClear(GL_COLOR_BUFFER_BIT);


    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();

    glColor3f(0.0, 0.0, 1.0);

   // drawCoordinates();

	int id = -1;
   //draw robot
	id = 0;
	glRotatef(rotations[id], 0, 0, 1);
        drawQuad(id); //Draw Stomach

        glPushMatrix(); //saves stomach 
        
		id = 1;
		glTranslatef(-1.5f, -3.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
        glScalef(0.25f, 1.0f, 0.0f);
        drawQuad(id); //Draw left thigh
		glPopMatrix();

		id = 2;
		glTranslatef(-0.5f, -3.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.25f, 1.0f, 0.0f);
		drawQuad(id); //draw left leg
		glPopMatrix();


		id = 3;
        glTranslatef(-2.5f, -2.25f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
        glScalef(1.5f, 0.5f, 0.0f);
        drawQuad(id); //draw left foot
		glPopMatrix();


		id = 4;
        glPopMatrix();//loads stomach
        glPushMatrix();//saves stomach
        glTranslatef(1.5f, -3.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
        glScalef(0.25f, 1.0f, 0.0f);
        drawQuad(id); //Draw right thigh
		glPopMatrix();


		id = 5;
		glTranslatef(0.5f, -3.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.25f, 1.0f, 0.0f);
		drawQuad(id); //draw right leg
		glPopMatrix();


		id = 6;       
        glTranslatef(2.5f, -2.25f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
        glScalef(1.5f, 0.5f, 0.0f);
        drawQuad(id); //draw right foot
		glPopMatrix();


        glPopMatrix(); //loads stomach
        // glPushMatrix();//saves stomach
		id = 7;
        glTranslatef(0.0f, 3.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
        glScalef(1.5f, 1.0f, 0.0f);
        drawQuad(id); //Draw Chest
		glPopMatrix();

       
        glPushMatrix();//saves chest

		id = 8;
        glTranslatef(0.0, 3.0, 0.0);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.25 , 1.0, 0.0);
        drawQuad(id); //draws neck
		glPopMatrix();

		id = 9;
        glTranslatef(0.0, 3.45, 0.0);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
        glScalef(1.75, 1.25, 0.0);
        drawQuad(id); //draws head
		glPopMatrix();

   

	   id = 10;
        glPopMatrix();//loads chest
		glPushMatrix();//saves chest
		glTranslatef(-4.0f, 0.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.5f, 0.5f, 0.0f);
		drawQuad(id); //draw left arm
		glPopMatrix();


		id = 11;
		glTranslatef(-3.0f, 0.5f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		drawQuad(id); //draw left forarm

		id = 12;
		glTranslatef(-2.5f, 0.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.25f, 1.0f, 0.0f);
		drawQuad(id); //draw left hand
		glPopMatrix();


		id = 13;
		glPopMatrix(); //load chest
		glTranslatef(4.0f, 0.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.5f, 0.5f, 0.0f);
		drawQuad(id); //draw right arm
		glPopMatrix();


		id = 14;
		glTranslatef(3.0f, 0.5f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		drawQuad(id); //draw right forarm

		id = 15;
		glTranslatef(2.5f, 0.0f, 0.0f);
		glRotatef(rotations[id], 0, 0, 1);
		glPushMatrix();
		glScalef(0.25f, 1.0f, 0.0f);
		drawQuad(id); //draw right hand
		glPopMatrix();


    //glRotatef(g_angle, 0.0f, 0.0f, 1.0f);
   // for (int i = 0; i < 10; i++) {
   //     glTranslatef(1.0f, 0.0f, 0.0f);
   //     drawQuad();
   // }

    glutSwapBuffers();
}

// called when window is first created or when window is resized
void reshape(int w, int h)
{
    // update thescreen dimensions
    width = w;
    height = h;

    //do an orthographic parallel projection, limited by screen/window size
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    // gluOrtho2D(0.0, 10.0, 0.0, 10.0);
    gluOrtho2D(-20.0, 20.0, -20.0, 20.0);

    /* tell OpenGL to use the whole window for drawing */
    glViewport(0, 0, (GLsizei)width, (GLsizei)height);
    //glViewport((GLsizei) width/2, (GLsizei) height/2, (GLsizei) width, (GLsizei) height);

    glutPostRedisplay();
}


void keyBoard(unsigned char key, int x, int y) {
	if (key == 'a')
		rotations[curID] += 5.0f;
	else if (key == 'd')
		rotations[curID] -= 5.0f;

	//use W and S to change currID
	if (key == 's' && curID<15)
		curID++;
	else if (key == 'w' && curID != 0)
		curID--;

	glutPostRedisplay();
}

void idle() {
    curTime = glutGet(GLUT_ELAPSED_TIME); // returns the number of milliseconds since glutInit() was called.
    float deltaTime = (float)(curTime - preTime) / 1000.0f; // frame-different time in seconds 
    preTime = curTime;

    g_angle += 45.0f * deltaTime;


    if (g_angle > 360.0f)
        g_angle -= 360.0f;

    glutPostRedisplay();
}

int main(int argc, char* argv[])
{
    // before create a glut window,
    // initialize stuff not opengl/glut dependent
    init();

    //initialize GLUT, let it extract command-line GLUT options that you may provide
    //NOTE that the '&' before argc
    glutInit(&argc, argv);

    // specify as double bufferred can make the display faster
    // Color is speicfied to RGBA, four color channels with Red, Green, Blue and Alpha(depth)
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);

    //set the initial window size */
    glutInitWindowSize((int)width, (int)height);

    // create the window with a title
    glutCreateWindow("Rotate Quad Push Pop OpenGL Program");

    /* --- register callbacks with GLUT --- */

    //register function to handle window resizes
    glutReshapeFunc(reshape);

    //register function that draws in the window
    glutDisplayFunc(display);

    glutIdleFunc(idle);

	glutKeyboardFunc(keyBoard);

    //start the glut main loop
    glutMainLoop();

    return 0;
}