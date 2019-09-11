const { src, dest, parallel, series, watch } = require('gulp');
const del = require('del');
const path = require('path');
const ssi = require('gulp-ssi');
const lec = require('gulp-line-ending-corrector');

function clean() {
  return del(['dist/**']);
}

function copyReveal() {
  const rbase = 'node_modules/reveal.js';
  return src(
          [
            path.join(rbase, 'css/**/*.css'), path.join(rbase, 'js/**/*.js'),
            path.join(rbase, 'lib/**/*'), path.join(rbase, 'plugin/**/*')
          ],
          {base: rbase})
      .pipe(dest('dist'));
}

function copyJquery() {
  return src('node_modules/jquery/dist/jquery.min.js').pipe(dest('dist/js'));
}

function copyHtml() {
  return src('*.html').pipe(ssi()).pipe(dest('dist'));
}

function copyCss() {
  return src('*.css').pipe(dest('dist/css'));
}

function copyHeaders() {
  return src(['headers.js']).pipe(dest('dist/js'));
}

function copyImages(cb) {
  return src('images/**/*').pipe(dest('dist/images'));
}

function copyMarkdown(cb) {
  return src(['*.md', '!README.md'])
    .pipe(ssi())
    .pipe(lec())
    .pipe(dest('dist'));
}

const defaultTasks = series(parallel(copyHtml, copyCss, copyHeaders, copyImages, copyMarkdown), copyReveal, copyJquery);

exports.clean = clean;
exports.default = defaultTasks;

exports.watch = function() {
  watch(
      ['*.html', '*.css', 'headers.js', 'images/**/*', '*.md'],
      { ignoreInitial: false },
      defaultTasks);
}
