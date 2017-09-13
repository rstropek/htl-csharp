const gulp = require('gulp');
const del = require('del');
const path = require('path');
const ssi = require('gulp-ssi');

gulp.task('clean', () => {
  return del(['dist/**']);
});

gulp.task('copy-reveal', () => {
  const rbase = 'node_modules/reveal.js';
  gulp.src(
          [
            path.join(rbase, 'css/**/*.css'), path.join(rbase, 'js/**/*.js'),
            path.join(rbase, 'lib/**/*'), path.join(rbase, 'plugin/**/*')
          ],
          {base: rbase})
      .pipe(gulp.dest('dist'));
});

gulp.task('copy-jquery', () => {
  gulp.src('node_modules/jquery/dist/jquery.min.js').pipe(gulp.dest('dist/js'));
});

gulp.task('copy-presentation', () => {
  gulp.src('*.html').pipe(ssi()).pipe(gulp.dest('dist'));
  gulp.src('*.css').pipe(gulp.dest('dist/css'));
  gulp.src(['headers.js']).pipe(gulp.dest('dist/js'));
  gulp.src('images/**/*').pipe(gulp.dest('dist/images'));
  gulp.src(['*.md', '!README.md']).pipe(ssi()).pipe(gulp.dest('dist'));
});

gulp.task('default', ['copy-presentation', 'copy-reveal', 'copy-jquery']);

gulp.task('watch', ['default'], () => {
  gulp.watch(
      ['*.html', '*.css', 'headers.js', 'images/**/*', '*.md'], ['default']);
});
